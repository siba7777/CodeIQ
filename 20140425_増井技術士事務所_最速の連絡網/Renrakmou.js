var student = 14;
var i, teacher, min, tmp;

// �����l�ݒ�
min = student * student;
teacher = student;

// �搶���A���ł���񐔂��A0�`���k���̍ŏ��̘A���Ԗԗ����Ԃ����߂�
// �A���Ԗԗ����ԁA�搶�̘A���񐔁A�o�����ŏ��ƂȂ�P�[�X��T������
for (i = 0; i <= student; ++i) {
    tmp = renraku(student, i - 1, 1, 1, 0, min);
    if (tmp < min) {
        min = tmp;
        teacher = i;
    } else if (tmp == min) {
        if (i < teacher) teacher = i;
    }
}
console.log("�c������܂ł̍ŒZ���ԁ�%d���A�搶���d�b����񐔁�%d��", min, teacher);


/**
* �I�������ɒB����܂ŁA�A���ς݂̐��k���S���d�b����
* �I�������ɒB������A�c��̐��k�֘A�����鎞�ԂƁA�搶�ւ̘A�����鎞�Ԃ̍ŏ��l�����߂�
*
* @param {int} sum ���k�̐�
* @param {int} teacher �搶���A���ł����
* @param {int} n �o�ߎ���
* @param {int} now �A���ς݂̐��k��
* @param {int} recent �O�̎��ԂɘA���������k���i�搶���A�������l�͏����j
* @param {int} min ����܂ł̍ŏ��̘A���Ԗԗ�����
* @return {int} �ŏ��̘A���Ԗԗ�����
*/
function renraku(sum, teacher, n, now, recent, min) {
    //console.log("sum:%d teacher:%d n:%d now:%d, recent:%d, min:%d", sum, teacher, n, now, recent, min);

    var tel, ttel, ret, tmp, rest;
    ret = min;
    ttel = 0 < teacher ? 1 : 0; //�搶���A�����邩�ۂ��i�搶�̑��M�j

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