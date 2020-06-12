using AcFunCommentControl.Models;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace AcFunCommentControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AcFunCommentControl : UserControl
    {
        private const RegexOptions Options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
        private static readonly Regex AtReg = new Regex(@"\[at uid=\d+\](?<at>.*?)\[\/at\]", Options);
        private static readonly Regex EmotReg = new Regex(@"\[emot=(?<emot>.*?)\/\]", Options);
        private static readonly Regex ImgReg = new Regex(@"\[img=图片\](?<img>.*?)\[\/img\]", Options);
        private static readonly Regex ColorReg = new Regex(@"\[color=(?<color>.*?)\](?<content>.*?)\[\/color\]", Options);
        private static readonly Regex BoldReg = new Regex(@"\[b\](?<bold>.*?)\[\/b\]", Options);
        private static readonly Regex ItalicReg = new Regex(@"\[i\](?<italic>.*?)\[\/i\]", Options);
        private static readonly Regex EmailReg = new Regex(@"\[email\](?<content>.*?)\[\/email\]", Options);
        private static readonly Regex FontReg = new Regex("<font color=\"(?<color>#[\\w\\d]{6})\">(?<content>.*?)</font>", Options);

        public AcFunCommentControl()
        {
            InitializeComponent();

            DataContextChanged += AcFunComment_DataContextChanged;

        }

        private void AcFunComment_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var comment = (Comment)e.NewValue;
            var content = comment.Content;
            var text = new StringBuilder();
            var index = 0;
            while (index < content.Length)
            {
                if (content[index] == '[')
                {
                    var processing = content.Substring(index);
                    if (AtReg.IsMatch(processing))
                    {
                        var (matched, offset) = DecodeTag(
                                processing,
                                AtReg,
                                text,
                                match => new Run { Text = match.Groups["at"].Value, Foreground = Brushes.Blue }
                            );
                        if (matched)
                        {
                            index += offset;
                            continue;
                        }
                    }
                    if (EmotReg.IsMatch(processing))
                    {
                        var (matched, offset) = DecodeTag(
                            processing,
                            EmotReg,
                            text,
                            match =>
                            {
                                var emot = match.Groups["emot"].Value.Split(',');

                                Uri uri;

                                if (emot[0] == "zt" && EmotIconModel.Emotions.ContainsKey(emot[1]))
                                {
                                    uri = EmotIconModel.Emotions[emot[1]];
                                }
                                else
                                {
                                    uri = new Uri($"https://cdn.aixifan.com/dotnet/20130418/umeditor/dialogs/emotion/images/{emot[0]}/{emot[1]}.gif");
                                }
                                return new InlineUIContainer(new Image { Source = new BitmapImage(uri), Width = 48, Height = 48 });
                            }
                        );
                        if (matched)
                        {
                            index += offset;
                            continue;
                        }
                    }
                    if (ImgReg.IsMatch(processing))
                    {
                        var (matched, offset) = DecodeTag(
                            processing,
                            ImgReg,
                            text,
                            match => new InlineUIContainer(new Image { Source = new BitmapImage(new Uri(match.Groups["img"].Value)), MaxWidth = 100, MaxHeight = 100 })
                        );
                        if (matched)
                        {
                            index += offset;
                            continue;
                        }
                    }

                }
                else if (content[index] == '<')
                {
                    var processing = content.Substring(index);

                    if (FontReg.IsMatch(processing))
                    {
                        var (matched, offset) = DecodeTag(
                            processing,
                            FontReg,
                            text,
                            match => new Run { Text = match.Groups["content"].Value, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(match.Groups["color"].Value)) }
                        );
                        if (matched)
                        {
                            index += offset;
                            continue;
                        }
                    }
                }
                text.Append(content[index++]);
            }
            if (text.Length > 0)
            {
                Block.Inlines.Add(new Run(text.ToString()));
            }
        }

        private (bool, int) DecodeTag(string content, Regex reg, StringBuilder text, Func<Match, Inline> process)
        {
            var match = reg.Match(content);
            if (match.Groups[0].Index == 0)
            {
                if (text.Length > 0) { Block.Inlines.Add(new Run(text.ToString())); text.Clear(); }
                Block.Inlines.Add(process(match));
                return (true, match.Length);
            }
            return (false, 0);
        }
    }
}