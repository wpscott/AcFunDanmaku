```java
String uid = ?;
String safety_id = UUID.randomUUID().toString();
String version_name = PackageInfo.versionName;

byte[] val1 = encode1(1, 1);
byte[] val2 = encode1(System.currentTimeMillis() / 1000, 4);
byte[] val3 = Base64.encodeToString({0, 1} + encode3(encode2(MD5(safety_id + uid))), 10).getBytes();
byte[] val4 = encode1(-1737194894, 4);
byte[] val5 = encode1(version_name.length(), 1);
byte[] val6 = version_name.getBytes();
byte[] val7 = encode1(safety_id.length(), 1);
byte[] val8 = safety_id.getBytes();
byte[] val9;
if (uid) {
    val9 = encode4(encode1(uid.length(), 1), uid.getBytes());
} else {
    val9 = encode1(0, 1);
}

Base64.encodeToString(encode4({0, 1}, KSecurity.atlasEncrypt(encode4(val1, val2, val3, val4, val5, val6, val7, val8, val9))), 10);
```
