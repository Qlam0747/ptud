package com.example.lab2;

import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.RadioGroup;
import androidx.appcompat.app.AppCompatActivity;
import java.util.ArrayList;

public class bai3 extends AppCompatActivity {
    private EditText etId, etName;
    private RadioGroup rgType;
    private ListView lvEmployees;
    private ArrayList<Employee> employees;
    private ArrayAdapter<Employee> adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_bai3);

        etId = findViewById(R.id.etMaNV);
        etName = findViewById(R.id.etTenNV);
        rgType = findViewById(R.id.rgLoaiNV);
        lvEmployees = findViewById(R.id.listView3);

        employees = new ArrayList<>();
        adapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_1, employees);
        lvEmployees.setAdapter(adapter);
    }

    public void addNewEmployee(View view) {
        int radId = rgType.getCheckedRadioButtonId();
        String id = etId.getText().toString();
        String name = etName.getText().toString();
        Employee employee;
        if (radId == R.id.rbChinhThuc) {
            employee = new EmployeeFulltime(id, name);
        } else {
            employee = new EmployeeParttime(id, name);
        }
        employees.add(employee);
        adapter.notifyDataSetChanged();
    }
}

abstract class Employee {
    private String id;
    private String fullname;
private boolean manager;
    public Employee(String Id, String name, boolean ismanager) {

        this.id = id;
        this.fullname = name;
        this.manager = ismanager;
    }

    public abstract double tinhLuong();

    @Override
    public String toString() {
        // Cập nhật thông tin hiển thị tùy theo yêu cầu cụ thể của đề bài
        return id + " - " + fullname;
    }

    public String getFullName() {
        return fullname;
    }

    public boolean isManager() {
        return manager;
    }
}

class EmployeeFulltime extends Employee {
    public EmployeeFulltime(String id, String name) {
        super(id, name, true);
    }

    @Override
    public double tinhLuong() {
        // Tính toán lương cho nhân viên chính thức
        return 5000; // Giả sử lương cơ bản là 5000
    }
}

class EmployeeParttime extends Employee {
    public EmployeeParttime(String id, String name) {
        super(id, name, true);
    }

    @Override
    public double tinhLuong() {
        // Tính toán lương cho nhân viên thời vụ
        return 3000; // Giả sử lương cơ bản là 3000
    }
}
