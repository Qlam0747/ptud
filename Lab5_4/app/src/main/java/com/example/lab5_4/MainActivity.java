package com.example.lab5_4;

import android.media.MediaPlayer;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import androidx.appcompat.app.AppCompatActivity;
import java.util.concurrent.TimeUnit;

public class MainActivity extends AppCompatActivity {

    private Button btnPlay, btnPrev, btnNext;
    private TextView tvStatus, tvTime, tvCurrentSong;
    private ListView listView;
    private MediaPlayer mediaPlayer;
    private boolean isPlaying = false;
    private int[] songIds = {R.raw.ctcht, R.raw.htca, R.raw.nt};
    private String[] songNames = {"Chúng ta của hiện tại", "Hãy trao cho anh", "Nàng thơ"}; // Add more song names here
    private int currentSongIndex = -1;
    private Handler handler = new Handler();
    private Runnable updateTimeTask;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        btnPlay = findViewById(R.id.btnPlay);
        btnPrev = findViewById(R.id.btnPrev);
        btnNext = findViewById(R.id.btnNext);
        tvStatus = findViewById(R.id.tvStatus);
        tvTime = findViewById(R.id.tvTime);
        tvCurrentSong = findViewById(R.id.tvCurrentSong);
        listView = findViewById(R.id.listView);

        ArrayAdapter<String> adapter = new ArrayAdapter<>(this,
                android.R.layout.simple_list_item_1, songNames);
        listView.setAdapter(adapter);

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                currentSongIndex = position;
                playMusic();
            }
        });

        btnPlay.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (currentSongIndex != -1) {
                    if (!isPlaying) {
                        playMusic();
                    } else {
                        stopMusic();
                    }
                }
            }
        });

        btnPrev.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                playPrevious();
            }
        });

        btnNext.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                playNext();
            }
        });
    }

    private void playMusic() {
        if (mediaPlayer != null) {
            mediaPlayer.stop();
            mediaPlayer.release();
        }
        mediaPlayer = MediaPlayer.create(this, songIds[currentSongIndex]);
        mediaPlayer.start();
        isPlaying = true;
        btnPlay.setText("Stop Music");
        tvStatus.setText("Playing: " + songNames[currentSongIndex]);
        tvCurrentSong.setText(songNames[currentSongIndex]);
        updateTimeTask = new Runnable() {
            @Override
            public void run() {
                if (mediaPlayer != null && isPlaying) {
                    int currentPosition = mediaPlayer.getCurrentPosition();
                    int duration = mediaPlayer.getDuration();
                    tvTime.setText(formatTime(currentPosition) + " / " + formatTime(duration));
                    handler.postDelayed(this, 1000);
                }
            }
        };
        handler.post(updateTimeTask);
        mediaPlayer.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {
            @Override
            public void onCompletion(MediaPlayer mp) {
                playNext();
            }
        });
    }

    private void stopMusic() {
        if (mediaPlayer != null) {
            mediaPlayer.stop();
            mediaPlayer.release();
            mediaPlayer = null;
            isPlaying = false;
            btnPlay.setText("Play Music");
            tvStatus.setText("Music Stopped");
            tvCurrentSong.setText("No song playing");
            tvTime.setText("00:00 / 00:00");
            handler.removeCallbacks(updateTimeTask);
        }
    }

    private void playNext() {
        if (currentSongIndex < songIds.length - 1) {
            currentSongIndex++;
        } else {
            currentSongIndex = 0;
        }
        playMusic();
    }

    private void playPrevious() {
        if (currentSongIndex > 0) {
            currentSongIndex--;
        } else {
            currentSongIndex = songIds.length - 1;
        }
        playMusic();
    }

    private String formatTime(int millis) {
        return String.format("%02d:%02d",
                TimeUnit.MILLISECONDS.toMinutes(millis),
                TimeUnit.MILLISECONDS.toSeconds(millis) -
                        TimeUnit.MINUTES.toSeconds(TimeUnit.MILLISECONDS.toMinutes(millis)));
    }
}
