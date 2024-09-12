using Azure.Core;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using SchoolWep.Data.OptionsConfiguration;
using System.Drawing;

namespace SchoolWep.Services.SendEmailServices
{
    public class SendEmailServices : ISendEmailServices
    {
        public IOptions<EmailSetting> Emailsetting { get; }

        public SendEmailServices(IOptions<EmailSetting> emailsetting)
        {
            Emailsetting = emailsetting;
        }


        public async Task<bool> SendConfirmEmail(string EmailTo, string linkConfirm)
        {
            //Email Sender
            var EmailSender = new MimeMessage
            {
                Sender = MailboxAddress.Parse(Emailsetting.Value.Emial),
           
            };

            //EmailRecever
            EmailSender.To.Add(MailboxAddress.Parse(EmailTo));


            // CreateBodyOfMail
            var Builder = new BodyBuilder();

            var body = $@"
            <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        line-height: 1.6;
                        margin: 20px;
                        padding: 20px;
                        background-color: #f0f0f0;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        background-color: #ffffff;
                        padding: 20px;
                        border-radius: 5px;
                        box-shadow: 0 2px 5px rgba(0,0,0,0.1);
                    }}
                    h1 {{
                        color: #333333;
                        text-align: center;
                    }}
                    p {{
                        color: #666666;
                    }}
                    .footer {{
                        margin-top: 20px;
                        text-align: center;
                        color: #999999;
                    }}
                    .smiley {{
                        font-size: 24px;
                        text-align: center;
                    }}
                    .confirm-button {{
                        display: inline-block;
                        padding: 10px 20px;
                        background-color: #28a745;
                        color: white;
                        text-align: center;
                        text-decoration: none;
                        border-radius: 5px;
                        font-size: 16px;
                        font-weight: bold;
                        transition: background-color 0.3s ease;
                    }}
                    .confirm-button:hover {{
                        background-color: #218838;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>Confirmation of Registration</h1>
                    <p>Dear {EmailTo},</p>
                    <p>Thank you for registering with our application. <span class='smiley'>&#128512;</span></p>
                    <p>Please click the following link to confirm your registration:</p>
       
                    <a href='{linkConfirm}' class='confirm-button'>Confirm</a>
                    <p>If you did not request this registration, please disregard this email.</p>
                    <div class='footer'>
                        <p>Best regards,<br>Your Application Team <span class='smiley'>&#128077;</span></p>
                    </div>
                </div>
            </body>
            </html>";
            Builder.HtmlBody = body;

            EmailSender.Body = Builder.ToMessageBody();

            EmailSender.From.Add(new MailboxAddress(Emailsetting.Value.DispalyName, Emailsetting.Value.Emial));

            using var smptClient = new SmtpClient();

            try
            {
                await smptClient.ConnectAsync(Emailsetting.Value.Host, Emailsetting.Value.Port, SecureSocketOptions.StartTls);
                await smptClient.AuthenticateAsync(Emailsetting.Value.Emial, Emailsetting.Value.Password);
                await smptClient.SendAsync(EmailSender);
                return true;
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new InvalidOperationException("Error sending email", ex);
            }
            finally{
                await smptClient.DisconnectAsync(true);

            }

        }

        public async Task<bool> SendOTPResetPassword(string EmailTo, string OTP)
        {
            //Email Sender
            var EmailSender = new MimeMessage
            {
                Sender = MailboxAddress.Parse(Emailsetting.Value.Emial),

            };

            //EmailRecever
            EmailSender.To.Add(MailboxAddress.Parse(EmailTo));


            // CreateBodyOfMail
            var Builder = new BodyBuilder();

            string body = $@"
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            margin: 20px;
            padding: 20px;
            background-color: #f0f0f0;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }}
        h1 {{
            color: #333333;
            text-align: center;
        }}
        p {{
            color: #666666;
        }}
        .footer {{
            margin-top: 20px;
            text-align: center;
            color: #999999;
        }}
        .smiley {{
            font-size: 24px;
            text-align: center;
        }}
        .confirm-button {{
            display: inline-block;
            padding: 10px 20px;
            background-color: #28a745;
            color: white;
            text-align: center;
            text-decoration: none;
            border-radius: 5px;
            font-size: 16px;
            font-weight: bold;
            transition: background-color 0.3s ease;
        }}
        .confirm-button:hover {{
            background-color: #218838;
        }}
        .otp-code {{
            display: block;
            font-size: 18px;
            color: #333333;
            margin: 10px 0;
            text-align: center;
            padding: 10px;
            background-color: #e9ecef;
            border-radius: 5px;
            border: 1px solid #ced4da;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Password Reset Request</h1>
        <p>Dear {EmailTo},</p>
        <p>We received a request to reset your password. Please use the following code to reset your password:</p>
        <div class='otp-code'>{OTP}</div>
        <p>If you did not request a password reset, please disregard this email.</p>
        <div class='footer'>
            <p>Best regards,<br>Your Application Team <span class='smiley'>&#128077;</span></p>
        </div>
    </div>
</body>
</html>";
            Builder.HtmlBody = body;

            EmailSender.Body = Builder.ToMessageBody();

            EmailSender.From.Add(new MailboxAddress(Emailsetting.Value.DispalyName, Emailsetting.Value.Emial));

            using var smptClient = new SmtpClient();

            try
            {
                await smptClient.ConnectAsync(Emailsetting.Value.Host, Emailsetting.Value.Port, SecureSocketOptions.StartTls);
                await smptClient.AuthenticateAsync(Emailsetting.Value.Emial, Emailsetting.Value.Password);
                await smptClient.SendAsync(EmailSender);
                return true;
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new InvalidOperationException("Error sending email", ex);
            }
            finally
            {
                await smptClient.DisconnectAsync(true);

            }
        }
    }
}
