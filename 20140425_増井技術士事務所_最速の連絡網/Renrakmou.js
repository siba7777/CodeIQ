var student = 14;
var i, teacher, min, tmp;

// 初期値設定
min = student * student;
teacher = student;

// 先生が連絡できる回数が、0～生徒数の最少の連絡網網羅時間を求めて
// 連絡網網羅時間、先生の連絡回数、双方が最少となるケースを探索する
for (i = 0; i <= student; ++i) {
    tmp = renraku(student, i - 1, 1, 1, 0, min);
    if (tmp < min) {
        min = tmp;
        teacher = i;
    } else if (tmp == min) {
        if (i < teacher) teacher = i;
    }
}
console.log("把握するまでの最短時間＝%d分、先生が電話する回数＝%d回", min, teacher);


/**
* 終了条件に達するまで、連絡済みの生徒が全員電話する
* 終了条件に達したら、残りの生徒へ連絡する時間と、先生への連絡する時間の最少値を求める
*
* @param {int} sum 生徒の数
* @param {int} teacher 先生が連絡できる回数
* @param {int} n 経過時間
* @param {int} now 連絡済みの生徒数
* @param {int} recent 前の時間に連絡した生徒数（先生が連絡した人は除く）
* @param {int} min これまでの最少の連絡網網羅時間
* @return {int} 最少の連絡網網羅時間
*/
function renraku(sum, teacher, n, now, recent, min) {
    //console.log("sum:%d teacher:%d n:%d now:%d, recent:%d, min:%d", sum, teacher, n, now, recent, min);

    var tel, ttel, ret, tmp, rest;
    ret = min;
    ttel = 0 < teacher ? 1 : 0; //先生が連絡するか否か（先生の送信）

    if (sum <= now + ttel + now * (now + 1) / 2) {
        for (tel = now; recent - 1 <= tel; --tel) {
            if (recent <= tel) rest = sum - (now + ttel + tel * (tel + 1) / 2); 
            else rest = sum - (now + tel * (tel + 1) / 2);
            if (rest <= 0) {
                tmp = n + 1 + tel;
                if (tmp < ret) ret = tmp;
            }
            else break;
        }
    }
    else {
        tmp = renraku(sum, teacher - 1, n + 1, now + now + ttel, now, ret);
        if (tmp < ret) ret = tmp;
    }

    return ret;
}