//APIキー
var APIKEY = 'xh9ggj8ndnyaatt9';

//Callオブジェクト
var existingCall;

// Compatibility
navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia;

// 【問１】APIKEYを利用してPeerJSオブジェクトを生成してください
//  ※ログを確認するためにデバッグモード３で動かすこと
//  ※シグナリングサーバとSTUNサーバ、TURNサーバはPeerJSの標準設定のまま利用すること
var peer = new Peer({ key: APIKEY });

// 【問２】PeerIDを生成して$('#my-id')にセットするコードを書いて下さい 
peer.on('open', function (id) { $('#my-id').text(id); });

// 相手からのコールを受信したら自身のメディアストリームをセットして返答
peer.on('call', function (call) {
    //【問３】自分のビデオストリーム（window.localStream）を相手に送信するコードを書いて下さい
    call.answer(window.localStream);
    step3(call);
});

// エラーハンドラー
peer.on('error', function (err) {
    alert(err.message);
    step2();
});

// イベントハンドラー
$(function () {

    // 相手に接続
    $('#make-call').click(function () {
        //【問４】自分のビデオストリーム（window.localStream）を用いて相手に発信する為のコードを書いて下さい
        var call = peer.call($('#callto-id').val(), window.localStream);
        step3(call);

    });

    // 切断
    $('#end-call').click(function () {
        existingCall.close();
        step2();
    });

    // メディアストリームを再取得
    $('#step1-retry').click(function () {
        $('#step1-error').hide();
        step1();
    });

    // ステップ１実行
    step1();

});

// メディアストリームを取得する
function step1() {
    // 【問５】getUserMediaの以下のコード内のコメントを参考にコードを完成させえて下さい
    navigator.getUserMedia({ video: true, audio: true  }, function (stream) {
        // 【問６】【相手からのビデオストリームを$('my-video')にセットして下さい
        $('#my-video').prop('src', URL.createObjectURL(stream));
        // 【問７】取得したストリームを後で使うためにwindowオブジェクトに保存して下さい
        window.localStream = stream;
        step2();
    }, function () { $('#step1-error').show(); });
}

function step2() {
    //UIコントロール
    $('#step1, #step3').hide();
    $('#step2').show();
}

function step3(call) {
    // すでに接続中の場合はクローズする
    if (existingCall) {
        existingCall.close();
    }

    // 相手からのメディアストリームを待ち受ける
    call.on('stream', function (stream) {
        // 【問８】相手からのビデオストリームを$('their-video')にセットするコードを書いて下さい
        $('#their-video').prop('src', URL.createObjectURL(stream));
        $('#step1, #step2').hide();
        $('#step3').show();
    });

    // 相手がクローズした場合
    call.on('close', step2);

    // Callオブジェクトを保存
    existingCall = call;

    // UIコントロール
    $('#their-id').text(call.peer);
    $('#step1, #step2').hide();
    $('#step3').show();

}