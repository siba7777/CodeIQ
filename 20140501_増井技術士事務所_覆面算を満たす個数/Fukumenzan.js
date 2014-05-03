var nums = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
var chars = "READWITLKS";
var count = 0;

count = fukumenzan([], nums, chars, nums.length, count);
console.log("count:%d", count);

function fukumenzan(pre, post, chars, n, count) {
    // 最上位の文字は0不許可
    if (1 <= pre.length && pre[0] == 0) return count;
    if (5 <= pre.length && pre[4] == 0) return count;
    if (7 <= pre.length && pre[6] == 0) return count;
    if (10 <= pre.length && pre[9] == 0) return count;
    if (7 <= pre.length
            && (2 < Math.abs(pre[1] - pre[6])   // EとTの差は2以内
            || 2 * pre[0] + pre[6] + 2 < 10     // R+R+T+下位からの繰り上がり（最大2）は10以上（繰り上がり発生は必須）
            || pre[2] + pre[6] + 2 < 10         // A+T+下位からの繰り上がり（最大2）は10以上（繰り上がり発生は必須）
            || pre[1] + pre[2] + 2 < 10         // E+A+下位からの繰り上がり（最大2）は10以上（繰り上がり発生は必須）
            )) return count;
    if (10 <= pre.length && (2 < pre[9] - pre[4])) return count;    // S-Wの差は2以内

    var elem, i, rest, len, value, ret;
    ret = count;
    if (0 < n)
        for (i = 0, len = post.length; i < len; ++i) {
            rest = post.slice(0);
            elem = rest.splice(i, 1);
            ret = fukumenzan(pre.concat(elem), rest, chars, n - 1, ret);
        }
    else {
        value = transInt(chars, pre, "READ");
        value += transInt(chars, pre, "WRITE");
        value += transInt(chars, pre, "TALK");
        if (value == transInt(chars, pre, "SKILL")) {
            console.log("%d+%d+%d=%d",
                            transInt(chars, pre, "READ"),
                            transInt(chars, pre, "WRITE"),
                            transInt(chars, pre, "TALK"),
                            transInt(chars, pre, "SKILL"));
            ++ret;
        }
    }
    return ret;
}

function transInt(chars, nums, word) {
    var ret = "";
    var i, index;
    for ( i = 0; i < word.length; ++i) {
        index = chars.indexOf(word.charAt(i));
        if (index == -1) return 0;
        ret += nums[index];
    }
    return parseInt(ret, 10);
}

