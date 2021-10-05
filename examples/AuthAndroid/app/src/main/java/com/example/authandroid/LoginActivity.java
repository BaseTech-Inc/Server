package com.example.authandroid;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Cache;
import com.android.volley.DefaultRetryPolicy;
import com.android.volley.Network;
import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.BasicNetwork;
import com.android.volley.toolbox.DiskBasedCache;
import com.android.volley.toolbox.HurlStack;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;

import org.json.JSONObject;

import java.net.CookieManager;
import java.util.Map;

public class LoginActivity extends AppCompatActivity {

    Button btnLogin;
    EditText txtEmail, txtPassword;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        btnLogin = findViewById(R.id.btnLogin);
        txtEmail = findViewById(R.id.txtLoginEmail);
        txtPassword = findViewById(R.id.txtLoginPassword);

        btnLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String email = txtEmail.getText().toString();
                String password = txtPassword.getText().toString();

                RequestQueue requestQueue;

                // Instantiate the cache
                Cache cache = new DiskBasedCache(getCacheDir(), 1024 * 1024); // 1MB cap

                // Set up the network to use HttpURLConnection as the HTTP client.
                Network network = new BasicNetwork(new HurlStack());

                // Instantiate the RequestQueue with the cache and network.
                requestQueue = new RequestQueue(cache, network);

                // Start the queue
                requestQueue.start();

                String baseUrl = "https://tupaserver.azurewebsites.net";
                String url = baseUrl + "/api/Account/login?" + "email=" + email + "&password=" + password;

                StringRequest stringRequest = new StringRequest(Request.Method.POST, url,
                        new Response.Listener<String>() {
                            @Override
                            public void onResponse(String response) {
                                Context context = getApplicationContext();
                                CharSequence text = response;
                                int duration = Toast.LENGTH_SHORT;

                                Toast toast = Toast.makeText(context, text, duration);
                                toast.show();
                            }
                        },
                        new Response.ErrorListener() {
                            @Override
                            public void onErrorResponse(VolleyError error) {
                                // TODO: Handle error

                                NetworkResponse networkResponse = error.networkResponse;

                                if (networkResponse != null && networkResponse.data != null) {
                                    String jsonError = new String(networkResponse.data);

                                    Context context = getApplicationContext();
                                    CharSequence text = jsonError;
                                    int duration = Toast.LENGTH_SHORT;

                                    Toast toast = Toast.makeText(context, text, duration);
                                    toast.show();
                                }
                            }
                        }){

                    @Override
                    protected Response<String> parseNetworkResponse(NetworkResponse response) {
                        // since we don't know which of the two underlying network vehicles
                        // will Volley use, we have to handle and store session cookies manually
                        Log.i("response", response.headers.toString());
                        Map<String, String> responseHeaders = response.headers;
                        String rawCookies = responseHeaders.get("Set-Cookie");
                        Log.i("cookies", rawCookies);
                        return super.parseNetworkResponse(response);
                    }
                };

                stringRequest.setRetryPolicy(new DefaultRetryPolicy(0,
                        DefaultRetryPolicy.DEFAULT_MAX_RETRIES, DefaultRetryPolicy.DEFAULT_BACKOFF_MULT));

                // Add the request to the RequestQueue.
                requestQueue.add(stringRequest);
            }
        });
    }
}