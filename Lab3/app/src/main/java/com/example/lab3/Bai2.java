package com.example.lab3;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import java.util.List;

public class Bai2 extends AppCompatActivity {

    private ContactAdapter adapter;
    private DatabaseHandle db;
    private List<DatabaseHandle.Contact> contacts;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_bai2);

        db = new DatabaseHandle(this);

        // Insert some contacts
        Log.d("Insert: ", "Inserting ..");
        db.addContact(new DatabaseHandle.Contact("Ravi", "9100000000"));
        db.addContact(new DatabaseHandle.Contact("Srinivas", "9199999999"));
        db.addContact(new DatabaseHandle.Contact("Tommy", "9522222222"));
        db.addContact(new DatabaseHandle.Contact("Karthik", "9533333333"));

        // Reading all contacts
        Log.d("Reading: ", "Reading all contacts..");
        contacts = db.getAllContacts();

        ListView listView = findViewById(R.id.listViewContacts);
        adapter = new ContactAdapter(this, contacts);
        listView.setAdapter(adapter);

        listView.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
            @Override
            public boolean onItemLongClick(AdapterView<?> parent, View view, int position, long id) {
                DatabaseHandle.Contact contact = contacts.get(position);
                db.deleteContact(contact);
                contacts.remove(position);
                adapter.notifyDataSetChanged();

                Toast.makeText(Bai2.this, "Deleted: " + contact.getName(), Toast.LENGTH_SHORT).show();
                return true;
            }
        });
    }
}
