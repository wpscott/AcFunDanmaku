```java
public static String mkey() {
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

    return Base64.encodeToString(encode4({0, 1}, KSecurity.atlasEncrypt(encode4(val1, val2, val3, val4, val5, val6, val7, val8, val9))), 10);
}
```

<details>
    <summary>encode1</summary>
    
```java
public static byte[] encode1(long val, int len) {
    byte[] arr = new byte[len];
    for (int i = len - 1; i >= 0; i--) {
        arr[i] = (byte) ((int) (255 & val));
        val >>= 8;
    }
    return arr;
}
```
</details>
<details>
    <summary>encode2</summary>
    
```java
public static byte[] encode2(String str) throws IllegalArgumentException {
    if (str == null || str.length() % 2 == 1) {
        throw new IllegalArgumentException("hexBinary needs to be even-length: " + str);
    }
    char[] charArray = str.toCharArray();
    int length = charArray.length;
    byte[] bArr = new byte[length / 2];
    for (int i = 0; i < length; i += 2) {
        bArr[i / 2] = (byte) ((Character.digit(charArray[i], 16) << 4) + Character.digit(charArray[i + 1], 16));
    }
    return bArr;
}
```
</details>
<details>
    <summary>encode3</summary>
    
```java
public static String encode3(byte[] arr, int offset, int length) {
    if (arr == null) {
        throw new NullPointerException("bytes is null");
    } else if (offset < 0 || offset + length > arr.length) {
        throw new IndexOutOfBoundsException();
    } else {
        int new_length = length * 2;
        char[] result = new char[new_length];
        char[] const = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};
        int j = 0;
        for (int i = 0; i < length; i++) {
            int b = arr[i + offset] & 255;
            cArr[j++] = const[b >> 4];
            cArr[j++] = const[b & 15];
        }
        return new String(result, 0, new_length);
    }
}
```
</details>
<details>
    <summary>encode4</summary>
    
```java
public static byte[] encode4(byte[]... arrs) {
    int len = 0;
    for (byte[] arr : arrs) {
        len += arr.length;
    }
    byte[] result = new byte[len];
    int offset = 0;
    for (byte[] arr : arrs) {
        System.arraycopy(arr, 0, result, offset, arr.length);
        offset += arr.length;
    }
    return result;
}
```
</details>
