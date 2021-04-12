using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace MailKitSend
{
  class Program
  {
    static void Main(string[] args)
    {
      // メールの送信に必要な情報
      var smtpHostName = "[SMTP サーバー名]";
      var smtpPort = 587;                         // or 25
      var smtpAuthUser = "[認証ユーザー名]";
      var smtpAuthPassword = "[認証パスワードまたはアプリパスワード]";


      // メールの内容
      var from = "[送信者メールアドレス]";
      var to = "[送り先メールアドレス]";

      var subject = "テストメールの件名です。";
      var body = "テストメールの本文です。\n改行です。";
      var textFormat = TextFormat.Text;


      // MailKit におけるメールの情報
      var message = new MimeMessage();

      // 送り元情報  
      message.From.Add(MailboxAddress.Parse(from));

      // 宛先情報  
      message.To.Add(MailboxAddress.Parse(to));

      // メールの送り先で送信者、受信者の表示名を変える場合
      //message.From.Add(new MailboxAddress("送信者の名前", from));
      //message.To.Add(new MailboxAddress("送り先の名前", to));

      // CC, BCC を入れる場合
      //message.Cc.Add(MailboxAddress.Parse(cc));
      //message.Bcc.Add(MailboxAddress.Parse(bcc));

      // 表題  
      message.Subject = subject;

      // 内容  
      var textPart = new TextPart(textFormat)
      {
        Text = body,
      };
      message.Body = textPart;

      // 添付ファイルを送信する場合
      //// using System.IO; 必要
      //var filePath = @"[ローカルに保存されているファイルパス].jpg";   // 例として JPEG ファイル指定
      //var buffer = File.ReadAllBytes(filePath);

      //var builder = new BodyBuilder();

      //// テキストか HTML で設定するプロパティが異なる
      //if (textFormat == TextFormat.Plain)
      //{
      //  builder.TextBody = body;
      //}
      //else
      //{
      //  builder.HtmlBody = body;
      //}

      //// 添付ファイルを追加
      //builder.Attachments.Add(Path.GetFileName(filePath), buffer, new ContentType("image", "jpg"));

      //message.Body = builder.ToMessageBody();


      using var client = new SmtpClient();

      // SMTPサーバに接続  
      client.Connect(smtpHostName, smtpPort, SecureSocketOptions.Auto);

      if (string.IsNullOrEmpty(smtpAuthUser) == false)
      {
        // SMTPサーバ認証  
        client.Authenticate(smtpAuthUser, smtpAuthPassword);
      }

      // 送信  
      client.Send(message);

      // 切断  
      client.Disconnect(true);
    }
  }
}
