#!/bin/bash

iterations=1000

make_get_request() {
    curl -X GET http://localhost:5241/read
    echo
}

make_post_request() {
    curl -X POST http://localhost:5241/write
}

export -f make_get_request
export -f make_post_request

# Loop to repeat the requests
for ((i=1; i<=$iterations; i++)); do
    # Run requests in parallel
    parallel --jobs 2 ::: make_get_request make_post_request >> responses.txt
done
