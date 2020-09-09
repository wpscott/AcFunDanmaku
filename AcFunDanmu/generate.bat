@ECHO  OFF
IF "%1" == "java" (protoc -Iprotos\im.basic --java_out=.\Models protos\im.basic\*.proto & protoc -Iprotos\zt.live.interactive --java_out=.\Models protos\zt.live.interactive\*.proto) ELSE ^
IF "%1" == "cpp" (protoc -Iprotos\im.basic --cpp_out=.\Models protos\im.basic\*.proto & protoc -Iprotos\zt.live.interactive --cpp_out=.\Models protos\zt.live.interactive\*.proto) ELSE ^
IF "%1" == "js" (protoc -Iprotos\im.basic --js_out=.\Models protos\im.basic\*.proto & protoc -Iprotos\zt.live.interactive --js_out=.\Models protos\zt.live.interactive\*.proto) ELSE ^
IF "%1" == "objc" (protoc -Iprotos\im.basic --objc_out=.\Models protos\im.basic\*.proto & protoc -Iprotos\zt.live.interactive --objc_out=.\Models protos\zt.live.interactive\*.proto) ELSE ^
IF "%1" == "php" (protoc -Iprotos\im.basic --php_out=.\Models protos\im.basic\*.proto & protoc -Iprotos\zt.live.interactive --php_out=.\Models protos\zt.live.interactive\*.proto) ELSE ^
IF "%1" == "python" (protoc -Iprotos\im.basic --python_out=.\Models protos\im.basic\*.proto & protoc -Iprotos\zt.live.interactive --python_out=.\Models protos\zt.live.interactive\*.proto) ELSE ^
IF "%1" == "ruby" (protoc -Iprotos\im.basic --ruby_out=.\Models protos\im.basic\*.proto & protoc -Iprotos\zt.live.interactive --ruby_out=.\Models protos\zt.live.interactive\*.proto) ELSE ^
IF "%1" == "go" (protoc -Iprotos\im.basic --go_out=.\Models protos\im.basic\*.proto & protoc -Iprotos\zt.live.interactive --go_out=.\Models protos\zt.live.interactive\*.proto) ELSE ^
IF "%1" == "csharp" (protoc -Iprotos\im.basic --csharp_out=.\Models protos\im.basic\*.proto & protoc -Iprotos\zt.live.interactive --csharp_out=.\Models protos\zt.live.interactive\*.proto) ELSE ^
ECHO Please enter output format: java/cpp/js/objc/php/python/ruby/csharp/go(need install google.golang.org/protobuf/cmd/protoc-gen-go)