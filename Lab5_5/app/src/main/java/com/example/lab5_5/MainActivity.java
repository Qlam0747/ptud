package com.example.lab5_5;

import android.net.Uri;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

import com.google.android.exoplayer2.ExoPlayer;
import com.google.android.exoplayer2.MediaItem;
import com.google.android.exoplayer2.Player;

import java.util.concurrent.TimeUnit;

import io.reactivex.rxjava3.android.schedulers.AndroidSchedulers;
import io.reactivex.rxjava3.core.Observable;
import io.reactivex.rxjava3.disposables.CompositeDisposable;
import io.reactivex.rxjava3.disposables.Disposable;
import io.reactivex.rxjava3.functions.Consumer;

public class MainActivity extends AppCompatActivity {

    private Button btnPlay, btnPrev, btnNext;
    private TextView tvStatus, tvTime, tvCurrentSong;
    private ListView listView;
    private ExoPlayer player;
    private boolean isPlaying = false;
    private int[] songIds = {R.raw.song1, R.raw.song2, R.raw.song3}; // Add more songs here
    private String[] songNames = {"Song 1", "Song 2", "Song 3"}; // Add more song names here
    private int currentSongIndex = -1;
    private CompositeDisposable disposables = new CompositeDisposable();
    private Disposable updateTimeDisposable;

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

        player = new ExoPlayer.Builder(this).build();

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

        player.addListener(new Player.Listener() {
            @Override
            public void onPlaybackStateChanged(int playbackState) {
                if (playbackState == Player.STATE_ENDED) {
                    playNext();
                }
            }
        });
    }

    private void playMusic() {
        if (player != null) {
            player.stop();
        }
        Uri uri = Uri.parse("android.resource://" + getPackageName() + "/" + songIds[currentSongIndex]);
        MediaItem mediaItem = MediaItem.fromUri(uri);
        player.setMediaItem(mediaItem);
        player.prepare();
        player.play();
        isPlaying = true;
        btnPlay.setText("Stop Music");
        tvStatus.setText("Playing: " + songNames[currentSongIndex]);
        tvCurrentSong.setText(songNames[currentSongIndex]);

        if (updateTimeDisposable != null && !updateTimeDisposable.isDisposed()) {
            updateTimeDisposable.dispose();
        }
        updateTimeDisposable = Observable.interval(1, TimeUnit.SECONDS)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Consumer<Long>() {
                    @Override
                    public void accept(Long aLong) throws Throwable {
                        int currentPosition = (int) player.getCurrentPosition();
                        int duration = (int) player.getDuration();
                        tvTime.setText(formatTime(currentPosition) + " / " + formatTime(duration));
                    }
                });
        disposables.add(updateTimeDisposable);
    }

    private void stopMusic() {
        if (player != null) {
            player.stop();
        }
        isPlaying = false;
        btnPlay.setText("Play Music");
        tvStatus.setText("Music Stopped");
        tvCurrentSong.setText("No song playing");
        tvTime.setText("00:00 / 00:00");
        if (updateTimeDisposable != null) {
            updateTimeDisposable.dispose();
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

    @Override
    protected void onDestroy() {
        super.onDestroy();
        if (player != null) {
            player.release();
        }
        if (disposables != null && !disposables.isDisposed()) {
            disposables.dispose();
        }
    }
}
