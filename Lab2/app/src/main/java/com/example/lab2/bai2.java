package com.example.lab2;

import static com.example.lab2.R.*;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;

import java.util.ArrayList;

public class bai2 extends Activity {
    private ArrayList<String> items;
    private ArrayAdapter<String> adapter;
    private ListView listView;
    private EditText editText;
    private Button button;
    private TextView textView;

    @SuppressLint("MissingInflatedId")
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_bai2);

        listView = (ListView) findViewById(R.id.listView2);
        editText = (EditText) findViewById(R.id.editText2);
        button = (Button) findViewById(R.id.button1);
        textView = (TextView) findViewById(R.id.textView3);

        items = new ArrayList<String>();
        adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, items);
        listView.setAdapter(adapter);

        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String input = editText.getText().toString();
                if (!input.isEmpty()) {
                    items.add(input);
                    adapter.notifyDataSetChanged();
                    editText.setText("");
                }
            }
        });

        listView.setOnItemClickListener((parent, view, position, id) -> {
            String item = adapter.getItem(position);
            textView.setText("Vị trí: " + position + ", Giá trị: " + item);
        });

        listView.setOnItemLongClickListener((parent, view, position, id) -> {
            items.remove(position);
            adapter.notifyDataSetChanged();
            return true;
        });
    }
}
