package com.example.lab5_1;

import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.TextView;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import java.util.Random;

public class MainActivity extends AppCompatActivity {
    private ProgressBar pbFirst, pbSecond;
    private TextView tvMsgWorking, tvMsgReturned;
    private boolean isRunning;
    private int MAX_SEC;
    private int intTest;
    private Thread bgThread;
    private Handler handler;
    private Button btnStart;
    private void findViewByIds() {
        pbFirst = (ProgressBar) findViewById(R.id.pb_first);
        pbSecond = (ProgressBar) findViewById(R.id.pb_second);
        tvMsgWorking = (TextView) findViewById(R.id.tv_working);
        tvMsgReturned = (TextView) findViewById(R.id.tv_return);
        btnStart = (Button) findViewById(R.id.btn_start);
    }
    private void initVariables() {
        isRunning = false;
        final int MAX_SEC = 20; // Use final for constants
        intTest = 1; // Assuming intTest1 needs initialization to 0

        pbFirst.setMax(MAX_SEC);
        pbFirst.setProgress(0);

        // Init Views (consider using a loop for efficiency)
        pbFirst.setVisibility(View.GONE);
        pbSecond.setVisibility(View.GONE);

        handler = new Handler() {
            @Override
            public void handleMessage(Message msg) {
                super.handleMessage(msg);
                String returnedValue = (String) msg.obj;

                // Handle the returned value from the background thread (logic may vary)
                tvMsgReturned.setText(getString(R.string.returned_by_bg_thread) + returnedValue);
                pbFirst.incrementProgressBy(2);

                // Check for progress completion
                if (pbFirst.getProgress() == MAX_SEC) {
                    tvMsgReturned.setText(getString(R.string.done_background_thread_has_been_stopped));
                    tvMsgWorking.setText(getString(R.string.done)); // Use tvMsgWorking (assuming typo)
                    pbFirst.setVisibility(View.GONE);
                    pbSecond.setVisibility(View.GONE);
                    btnStart.setVisibility(View.VISIBLE);
                    isRunning = false;
                } else {
                    tvMsgWorking.setText(getString(R.string.working) + pbFirst.getProgress()); // Use string concatenation
                }
            }
        };
    }
    private void initBgThread() {

        bgThread = new Thread(new Runnable() {
            @Override
            public void run() {

                try {
                    for (int i = 0; i < MAX_SEC && isRunning; i++) {

                        // Sleep one second
                        Thread.sleep(1000);

                        Random rnd = new Random();
                        // This is a locally generated value
                        String data = "Thread value: " + (int) rnd.nextInt(101);
                        // We can see change (global) class variables
                        data += getString(R.string.global_value_seen) + " " + intTest;
                        intTest++;

                        // If thread is still alive send the message
                        if (isRunning) {

                            // Request a message token and put some data in it
                            Message msg = handler.obtainMessage(1, (String) data);
                            handler.sendMessage(msg);

                        }

                    }
                } catch (Throwable t) {

                }
            }
        });

    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        // Khởi tạo các biến
        findViewByIds();
        initVariables();
        // Xử lý sự kiện click
        btnStart.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                isRunning = true;
                pbFirst.setVisibility(View.VISIBLE);
                pbSecond.setVisibility(View.VISIBLE);
                btnStart.setVisibility(View.GONE);
                bgThread.start();
            }
        });
    }

    @Override
    protected void onStart() {
        super.onStart();
        // Khởi tạo luồng nền
        initBgThread();
    }

    @Override
    protected void onStop() {
        super.onStop();
        isRunning = false;
    }

}