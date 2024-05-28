package com.example.lab3;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;
import com.example.lab3.DatabaseHandle;

import java.util.List;

public class ContactAdapter extends ArrayAdapter<DatabaseHandle.Contact> {

    public ContactAdapter(Context context, List<DatabaseHandle.Contact> contacts) {
        super(context, 0, contacts);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        DatabaseHandle.Contact contact = getItem(position);

        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.list_item_contact, parent, false);
        }

        TextView tvName = convertView.findViewById(R.id.tvName);
        TextView tvPhone = convertView.findViewById(R.id.tvPhone);

        tvName.setText(contact.getName());
        tvPhone.setText(contact.getPhoneNumber());

        return convertView;
    }
}
