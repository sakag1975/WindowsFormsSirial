# 2018/02/10
## 環境作成
## 開発参考
[URL](https://code.msdn.microsoft.com/windowsapps/COM-howto-6c7ff269)

受信処理がスレッドで実装されているが受信用のデリゲートの登録がわからなかった
SerialPortProcessor.cs L73 `DataReceived()`はSerialPortイベントなはず

# 2018/02/11
イベントの登録方法を確認して、できるようになったが、Form1のテキストボックスに受信した文字を入れることができない

# 2018/02/12
デリゲートを使用することで解決



