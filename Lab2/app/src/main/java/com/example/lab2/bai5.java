package com.example.lab2;

import android.os.Bundle;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

public class bai5 extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_bai5);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });
    }
    public enum Thumbnail{
        Thumbnail1("Thumbnail 1", R.drawable.first_thumbnail),
        Thumbnail2("Thumbnail 2", R.drawable.second_thumbnail),
        Thumbnail3("Thumbnail 3", R.drawable.third_thumbnail),
        Thumbnail4("Thumbnail 4", R.drawable.fourth_thumbnail),
        ;


        private final String name;
        private final int Thumbnail;

        Thumbnail(String name, int Thumbnail) {
            this.name = name;
            this.Thumbnail = Thumbnail;
        }
        public String getName() {return name;}

        public int getThumbnail() {
            return Thumbnail;
        }
    }
}