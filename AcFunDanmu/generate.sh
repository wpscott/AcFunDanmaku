#!/bin/bash

case $1 in
    java)
	    protoc --java_out=./Models/im -I./protos/Im/Basic -I./protos/Im/Message -I./protos/Im/Cloud/Channel -I./protos/Im/Cloud/Config -I./protos/Im/Cloud/Data/Update -I./protos/Im/Cloud/Message -I./protos/Im/Cloud/Profile -I./protos/Im/Cloud/Search -I./protos/Im/Cloud/SessionFolder -I./protos/Im/Cloud/SessionTag -I./protos/Im/Cloud/Voice/Call $(find protos/im -type f -name "*.proto") && protoc --java_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    cpp)
	    protoc --cpp_out=./Models/im -I./protos/Im/Basic -I./protos/Im/Message -I./protos/Im/Cloud/Channel -I./protos/Im/Cloud/Config -I./protos/Im/Cloud/Data/Update -I./protos/Im/Cloud/Message -I./protos/Im/Cloud/Profile -I./protos/Im/Cloud/Search -I./protos/Im/Cloud/SessionFolder -I./protos/Im/Cloud/SessionTag -I./protos/Im/Cloud/Voice/Call $(find protos/im -type f -name "*.proto") && protoc --cpp_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    js)
	    protoc --js_out=./Models/im -I./protos/Im/Basic -I./protos/Im/Message -I./protos/Im/Cloud/Channel -I./protos/Im/Cloud/Config -I./protos/Im/Cloud/Data/Update -I./protos/Im/Cloud/Message -I./protos/Im/Cloud/Profile -I./protos/Im/Cloud/Search -I./protos/Im/Cloud/SessionFolder -I./protos/Im/Cloud/SessionTag -I./protos/Im/Cloud/Voice/Call $(find protos/im -type f -name "*.proto") && protoc --js_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    objc)
	    protoc --objc_out=./Models/im -I./protos/Im/Basic -I./protos/Im/Message -I./protos/Im/Cloud/Channel -I./protos/Im/Cloud/Config -I./protos/Im/Cloud/Data/Update -I./protos/Im/Cloud/Message -I./protos/Im/Cloud/Profile -I./protos/Im/Cloud/Search -I./protos/Im/Cloud/SessionFolder -I./protos/Im/Cloud/SessionTag -I./protos/Im/Cloud/Voice/Call $(find protos/im -type f -name "*.proto") && protoc --objc_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    php)
	    protoc --php_out=./Models/im -I./protos/Im/Basic -I./protos/Im/Message -I./protos/Im/Cloud/Channel -I./protos/Im/Cloud/Config -I./protos/Im/Cloud/Data/Update -I./protos/Im/Cloud/Message -I./protos/Im/Cloud/Profile -I./protos/Im/Cloud/Search -I./protos/Im/Cloud/SessionFolder -I./protos/Im/Cloud/SessionTag -I./protos/Im/Cloud/Voice/Call $(find protos/im -type f -name "*.proto") && protoc --php_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    python)
        protoc --python_out=./Models/im -I./protos/Im/Basic -I./protos/Im/Message -I./protos/Im/Cloud/Channel -I./protos/Im/Cloud/Config -I./protos/Im/Cloud/Data/Update -I./protos/Im/Cloud/Message -I./protos/Im/Cloud/Profile -I./protos/Im/Cloud/Search -I./protos/Im/Cloud/SessionFolder -I./protos/Im/Cloud/SessionTag -I./protos/Im/Cloud/Voice/Call $(find protos/im -type f -name "*.proto") && protoc --python_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    ruby)
        protoc --ruby_out=./Models/im -I./protos/Im/Basic -I./protos/Im/Message -I./protos/Im/Cloud/Channel -I./protos/Im/Cloud/Config -I./protos/Im/Cloud/Data/Update -I./protos/Im/Cloud/Message -I./protos/Im/Cloud/Profile -I./protos/Im/Cloud/Search -I./protos/Im/Cloud/SessionFolder -I./protos/Im/Cloud/SessionTag -I./protos/Im/Cloud/Voice/Call $(find protos/im -type f -name "*.proto") && protoc --ruby_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    go)
        protoc --go_out=./Models/im -I./protos/Im/Basic -I./protos/Im/Message -I./protos/Im/Cloud/Channel -I./protos/Im/Cloud/Config -I./protos/Im/Cloud/Data/Update -I./protos/Im/Cloud/Message -I./protos/Im/Cloud/Profile -I./protos/Im/Cloud/Search -I./protos/Im/Cloud/SessionFolder -I./protos/Im/Cloud/SessionTag -I./protos/Im/Cloud/Voice/Call $(find protos/im -type f -name "*.proto") && protoc --go_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    csharp)
        protoc --csharp_out=./Models/im -I./protos/Im/Basic -I./protos/Im/Message -I./protos/Im/Cloud/Channel -I./protos/Im/Cloud/Config -I./protos/Im/Cloud/Data/Update -I./protos/Im/Cloud/Message -I./protos/Im/Cloud/Profile -I./protos/Im/Cloud/Search -I./protos/Im/Cloud/SessionFolder -I./protos/Im/Cloud/SessionTag -I./protos/Im/Cloud/Voice/Call $(find protos/im -type f -name "*.proto") && protoc --csharp_out=./Models -I ./protos/zt.live.interactive $(find protos/zt.live.interactive -type f -name "*.proto")
        ;;
    *)
        echo "Please enter output format: java/cpp/js/objc/php/python/ruby/csharp/go(need install google.golang.org/protobuf/cmd/protoc-gen-go)"
        ;;
esac