package com.example.lab2;

import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ListView;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import java.util.ArrayList;

public class bai4 extends AppCompatActivity {
private EditText etid, etname;
private CheckBox check;
    private ListView lvEmployees;
    private ArrayList<Employee> employees;
    private ArrayAdapter<Employee> adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_bai4);
         etid = findViewById(R.id.editTextText);
         etname=findViewById(R.id.editTextText2);
         check=findViewById(R.id.checkBox);
         lvEmployees=findViewById(R.id.listView4);
        employees = new ArrayList<>();
        adapter = new EmployeeAdapter(this, R.layout.item_employee, employees);
        lvEmployees.setAdapter(adapter);

    }
    public void addEmployee(View view) {
        String id = etid.getText().toString();
        String name = etname.getText().toString();
        boolean isManager = check.isChecked();
        Employee employee = new Employee(id, name, isManager){
            @Override
            public double tinhLuong() {
                return 0;
            }
        };
        employees.add(employee);
        adapter.notifyDataSetChanged();
    }

}