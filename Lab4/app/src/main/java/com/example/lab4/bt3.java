package com.example.lab4;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.telephony.SmsManager;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.Switch;

import androidx.appcompat.app.AppCompatActivity;

import java.util.ArrayList;
import java.util.concurrent.locks.ReentrantLock;

public class bt3 extends AppCompatActivity {
    private ReentrantLock reentrantLock;
    private Switch swAutoResponse;
    private LinearLayout llButtons;
    private Button btnSafe, btnMayday;
    private ArrayList<String> requesters;
    private ArrayAdapter<String> adapter;
    private ListView lvMessages;
    private BroadcastReceiver broadcastReceiver;
    public static boolean isRunning;
    private SharedPreferences.Editor editor;
    private SharedPreferences sharedPreferences;
    private final String AUTO_RESPONSE = "auto_response";

    private void findViewsByIds() {
        swAutoResponse = findViewById(R.id.sw_auto_response);
        llButtons = findViewById(R.id.ll_buttons);
        lvMessages = findViewById(R.id.lv_messages);
        btnSafe = findViewById(R.id.btn_safe);
        btnMayday = findViewById(R.id.btn_mayday);
    }

    private void respond(String to, String response) {
        reentrantLock.lock();
        try {
            requesters.remove(to);
            adapter.notifyDataSetChanged();
        } finally {
            reentrantLock.unlock();
        }
        SmsManager sms = SmsManager.getDefault();
        sms.sendTextMessage(to, null, response, null, null);
    }

    public void respond(boolean ok) {
        String okString = getString(R.string.i_am_safe_and_well_worry_not);
        String notOkString = getString(R.string.tell_my_mother_i_love_her);
        String outputString = ok ? okString : notOkString;
        ArrayList<String> requestersCopy = (ArrayList<String>) requesters.clone();
        for (String to : requestersCopy) {
            respond(to, outputString);
        }
    }

    public void processReceiveAddresses(ArrayList<String> addresses) {
        for (String address : addresses) {
            if (!requesters.contains(address)) {
                reentrantLock.lock();
                try {
                    requesters.add(address);
                    adapter.notifyDataSetChanged();
                } finally {
                    reentrantLock.unlock();
                }
            }
            if (swAutoResponse.isChecked()) {
                respond(true);
            }
        }
    }

    private void handleOnClickListeners() {
        btnSafe.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                respond(true);
            }
        });
        btnMayday.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                respond(false);
            }
        });
        swAutoResponse.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (isChecked) {
                    llButtons.setVisibility(View.GONE);
                } else {
                    llButtons.setVisibility(View.VISIBLE);
                }
                // Save auto response setting
                editor.putBoolean(AUTO_RESPONSE, isChecked);
                editor.apply();
            }
        });
    }

    private void initBroadcastReceiver() {
        broadcastReceiver = new BroadcastReceiver() {
            @Override
            public void onReceive(Context context, Intent intent) {
                ArrayList<String> addresses = intent.getStringArrayListExtra(SmsReceiver.SMS_MESSAGE_ADDRESS_KEY);
                if (addresses != null) {
                    processReceiveAddresses(addresses);
                }
            }
        };
    }

    @Override
    protected void onResume() {
        super.onResume();
        isRunning = true;
        // Make sure broadcastReceiver was inited
        if (broadcastReceiver == null) {
            initBroadcastReceiver();
        }
        // RegisterReceiver
        IntentFilter intentFilter = new IntentFilter(SmsReceiver.SMS_FORWARD_BROADCAST_RECEIVER);
        registerReceiver(broadcastReceiver, intentFilter);
    }

    @Override
    protected void onStop() {
        super.onStop();
        isRunning = false;
        // UnregisterReceiver
        unregisterReceiver(broadcastReceiver);
    }

    private void initVariables() {
        sharedPreferences = getPreferences(MODE_PRIVATE);
        editor = sharedPreferences.edit();
        reentrantLock = new ReentrantLock();
        requesters = new ArrayList<>();
        adapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_1, requesters);
        lvMessages.setAdapter(adapter);

        // Load auto response setting
        boolean autoResponse = sharedPreferences.getBoolean(AUTO_RESPONSE, false);
        swAutoResponse.setChecked(autoResponse);
        if (autoResponse) {
            llButtons.setVisibility(View.GONE);
        }
        initBroadcastReceiver();

        ArrayList<String> addresses = getAddresses();
        processReceiveAddresses(addresses);
    }


    private ArrayList<String> getAddresses() {
        ArrayList<String> addresses = new ArrayList<>();
        return addresses;
    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        findViewsByIds();
        initVariables();
        handleOnClickListeners();
    }
}
