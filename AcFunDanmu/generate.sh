#!/bin/bash

case $1 in
    java)
	    protoc --java_out=./Models -I ./protos/im.basic -I ./protos/zt.live.interactive $(find protos -type f -name "*.proto")
        ;;
    cpp)
	    protoc --cpp_out=./Models -I ./protos/im.basic -I ./protos/zt.live.interactive $(find protos -type f -name "*.proto")
        ;;
    js)
	    protoc --js_out=./Models -I ./protos/im.basic -I ./protos/zt.live.interactive $(find protos -type f -name "*.proto")
        ;;
    objc)
	    protoc --objc_out=./Models -I ./protos/im.basic -I ./protos/zt.live.interactive $(find protos -type f -name "*.proto")
        ;;
    php)
	    protoc --php_out=./Models -I ./protos/im.basic -I ./protos/zt.live.interactive $(find protos -type f -name "*.proto")
        ;;
    python)
        protoc --python_out=./Models -I ./protos/im.basic -I ./protos/zt.live.interactive $(find protos -type f -name "*.proto")
        ;;
    ruby)
        protoc --ruby_out=./Models -I ./protos/im.basic -I ./protos/zt.live.interactive $(find protos -type f -name "*.proto")
        ;;
    go)
        protoc --go_out=./Models -I ./protos/im.basic -I ./protos/zt.live.interactive $(find protos -type f -name "*.proto")
        ;;
    csharp)
        protoc --csharp_out=./Models -I ./protos/im.basic -I ./protos/zt.live.interactive $(find protos -type f -name "*.proto")
        ;;
    *)
        echo "Please enter output format: java/cpp/js/objc/php/python/ruby/csharp/go(need install google.golang.org/protobuf/cmd/protoc-gen-go)"
        ;;
esac