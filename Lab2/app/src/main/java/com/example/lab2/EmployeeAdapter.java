package com.example.lab2;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import java.util.List;

public class EmployeeAdapter extends ArrayAdapter<Employee> {
    private Activity context;
    private int layoutID;

    public EmployeeAdapter(Activity context, int layoutID, List<Employee> objects) {
        super(context, layoutID, objects);
        this.context = context;
        this.layoutID = layoutID;
    }

    @Override
    public View getView(final int position, View convertView, ViewGroup parent) {
        ViewHolder holder;

        if (convertView == null) {
            convertView = LayoutInflater.from(context).inflate(layoutID, parent, false);
            holder = new ViewHolder();
            holder.tvFullName = convertView.findViewById(R.id.item_employee_tv_fullname);
            holder.tvPosition = convertView.findViewById(R.id.item_employee_tv_position);
            holder.ivManager = convertView.findViewById(R.id.item_employee_iv_manager);
            holder.llParent = convertView.findViewById(R.id.item_employee_ll_parent);
            convertView.setTag(holder);
        } else {
            holder = (ViewHolder) convertView.getTag();
        }

        Employee employee = getItem(position);

        holder.tvFullName.setText(employee != null ? employee.getFullName() : "");

        if (employee != null && employee.isManager()) {
            holder.ivManager.setVisibility(View.VISIBLE);
            holder.tvPosition.setVisibility(View.GONE);
        } else {
            holder.ivManager.setVisibility(View.GONE);
            holder.tvPosition.setVisibility(View.VISIBLE);
            holder.tvPosition.setText(context.getString(R.string.staff));
        }

        holder.llParent.setBackgroundResource(position % 2 == 0 ? R.color.white : R.color.light_blue);

        return convertView;
    }

    static class ViewHolder {
        TextView tvFullName;
        TextView tvPosition;
        ImageView ivManager;
        LinearLayout llParent;
    }
}