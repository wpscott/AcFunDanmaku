#!/bin/bash

case $1 in
    java)
	    protoc --java_out=./Models/im -I ./protos/im/Basic -I ./protos/im/Message $(find protos/im -type f -name "*.proto") && protoc --java_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    cpp)
	    protoc --cpp_out=./Models/im -I ./protos/im/Basic -I ./protos/im/Message $(find protos/im -type f -name "*.proto") && protoc --cpp_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    js)
	    protoc --js_out=./Models/im -I ./protos/im/Basic -I ./protos/im/Message $(find protos/im -type f -name "*.proto") && protoc --js_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    objc)
	    protoc --objc_out=./Models/im -I ./protos/im/Basic -I ./protos/im/Message $(find protos/im -type f -name "*.proto") && protoc --objc_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    php)
	    protoc --php_out=./Models/im -I ./protos/im/Basic -I ./protos/im/Message $(find protos/im -type f -name "*.proto") && protoc --php_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    python)
        protoc --python_out=./Models/im -I ./protos/im/Basic -I ./protos/im/Message $(find protos/im -type f -name "*.proto") && protoc --python_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    ruby)
        protoc --ruby_out=./Models/im -I ./protos/im/Basic -I ./protos/im/Message $(find protos/im -type f -name "*.proto") && protoc --ruby_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    go)
        protoc --go_out=./Models/im -I ./protos/im/Basic -I ./protos/im/Message $(find protos/im -type f -name "*.proto") && protoc --go_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    csharp)
        protoc --csharp_out=./Models/im -I ./protos/im/Basic -I ./protos/im/Message $(find protos/im -type f -name "*.proto") && protoc --csharp_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    *)
        echo "Please enter output format: java/cpp/js/objc/php/python/ruby/csharp/go(need install google.golang.org/protobuf/cmd/protoc-gen-go)"
        ;;
esac