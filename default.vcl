vcl 4.1;

import std;

backend default {
    .host = "varnish-test-api";
    .port = "80";
    .max_connections = 100;
    # .probe = {
    #     .url = "/";
    #     .interval  = 10s;
    #     .timeout   = 5s;
    #     .window    = 5;
    #     .threshold = 3;
    # }
    .connect_timeout        = 5s;
    .first_byte_timeout     = 90s;
    .between_bytes_timeout  = 2s;
}

sub vcl_backend_response {
    set beresp.ttl = 30s;
    set beresp.http.X-TTL = beresp.ttl;	
    set beresp.grace = 24h;
    return (deliver);
}

sub vcl_hit {
    set req.http.healthy = std.healthy(req.backend_hint);
    set req.http.X-Varnish-TTL = obj.ttl;

    if (obj.ttl >= 0s) {
        return (deliver);
    }

    set req.http.grace = req.url;
    if (std.healthy(req.backend_hint)) {
        if (obj.ttl + 2000s > 0s) {
            set req.http.grace = "normal(limited) "  + obj.ttl;
            return (deliver);
        } else {
            return (miss);
        }
    } else {
        if (obj.ttl + obj.grace > 0s) {
            set req.http.grace = "full "  + obj.ttl + " " + obj.grace ;
            return (deliver);
        } else {
            return (miss);
        }
    }
}