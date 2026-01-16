using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MahApps.Metro.IconPacks;
using ZYC.CoreToolkit;
using WTextBlock = Emoji.Wpf.TextBlock;

namespace ZYC.Automation.Core;

public class HybridIcon : ContentControl
{
    public static readonly DependencyProperty IconProperty
        = DependencyProperty.Register(nameof(Icon),
            typeof(string), typeof(HybridIcon),
            new PropertyMetadata(DefaultIcon, OnIconChanged));

    public static PackIconMaterialKind DefaultIconKind { get; set; } = PackIconMaterialKind.Bug;
    public static string DefaultIcon => DefaultIconKind.ToString();

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var hybridIcon = (HybridIcon)d;

        if (e.NewValue == null)
        {
            hybridIcon.SetFromMaterialIcon(DefaultIconKind);
            return;
        }

        var s = e.NewValue.ToString() ?? string.Empty;

        // 1) Try Material icon name first.
        if (Enum.TryParse<PackIconMaterialKind>(s, out var materialKind))
        {
            hybridIcon.SetFromMaterialIcon(materialKind);
            return;
        }

        // 2) Then try a Base64 image.
        //    Use a preallocated buffer + TryFromBase64String to avoid exceptions.
        if (LooksLikeBase64(s))
        {
            var requiredBufferSize = s.Length * 3 / 4;
            var buffer = new byte[requiredBufferSize];
            if (Convert.TryFromBase64String(s, buffer, out var bytesWritten))
            {
                if (bytesWritten != buffer.Length)
                {
                    Array.Resize(ref buffer, bytesWritten);
                }

                hybridIcon.SetFromByteArray(buffer);
                return;
            }
        }

        // 3) Emoji / emoji sequence.
        if (LooksLikeEmoji(s))
        {
            hybridIcon.SetFromEmoji(s);
            return;
        }

        // 4) Fallback: default Material icon.
        hybridIcon.SetFromMaterialIcon(DefaultIconKind);
    }

    private void SetFromByteArray(byte[] buffer)
    {
        Content?.TryDispose();

        var image = new Image
        {
            Stretch = Stretch.Uniform
        };

        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.StreamSource = new MemoryStream(buffer);
        bitmap.EndInit();
        bitmap.Freeze();

        image.Source = bitmap;
        Content = image;
    }

    private void SetFromMaterialIcon(PackIconMaterialKind kind)
    {
        Content?.TryDispose();

        Content = new PackIconMaterial
        {
            Kind = kind,
            VerticalAlignment = VerticalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            HorizontalContentAlignment = HorizontalAlignment.Center
        };
        // Optionally bind Foreground/FontSize as well.
        if (Content is PackIconMaterial pi)
        {
            pi.SetBinding(ForegroundProperty, new Binding(nameof(Foreground)) { Source = this });
            pi.SetBinding(FontSizeProperty, new Binding(nameof(FontSize)) { Source = this });
        }
    }

    private void SetFromEmoji(string emojiText)
    {
        Content?.TryDispose();

        var tb = new WTextBlock
        {
            Text = emojiText,
            TextAlignment = TextAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
            // Segoe UI Emoji provides color glyphs; the system falls back if missing.
            //FontFamily = new FontFamily("Segoe UI Emoji")
        };

        // Bind size and color (color may not affect color emoji; used as a fallback).
        tb.SetBinding(FontSizeProperty, new Binding(nameof(FontSize)) { Source = this });
        tb.SetBinding(ForegroundProperty, new Binding(nameof(Foreground)) { Source = this });

        // Hint the rendering path to use DirectWrite (usually the default).
        TextOptions.SetTextFormattingMode(tb, TextFormattingMode.Display);
        TextOptions.SetTextRenderingMode(tb, TextRenderingMode.Auto);

        Content = tb;
    }

    // ----------------- Helpers -----------------

    private static bool LooksLikeBase64(string s)
    {
        // Rough check: length is multiple of 4 and contains only Base64 chars to reduce pointless decoding attempts.
        if (s.Length < 8 || s.Length % 4 != 0)
        {
            return false;
        }

        foreach (var c in s)
        {
            var ok =
                (c >= 'A' && c <= 'Z') ||
                (c >= 'a' && c <= 'z') ||
                (c >= '0' && c <= '9') ||
                c == '+' || c == '/' || c == '=';
            if (!ok)
            {
                return false;
            }
        }

        return true;
    }

    private static bool LooksLikeEmoji(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return false;
        }

        // Allow complex sequences: ZWJ (200D), variation selectors (FE0F), skin tones (1F3FB..1F3FF),
        // composite flags, etc. Any emoji code point in the sequence qualifies as emoji.
        var e = StringInfo.GetTextElementEnumerator(s);
        while (e.MoveNext())
        {
            var element = e.GetTextElement();
            if (ContainsEmojiCodePoint(element))
            {
                return true;
            }
        }

        return false;
    }

    private static bool ContainsEmojiCodePoint(string textElement)
    {
        // Walk code points in the text element.
        for (var i = 0; i < textElement.Length; i++)
        {
            var codePoint = char.IsSurrogatePair(textElement, i)
                ? char.ConvertToUtf32(textElement, i++)
                : textElement[i];

            // Common emoji ranges (not exhaustive but covers most cases).
            if (IsEmojiCodePoint(codePoint))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsEmojiCodePoint(int cp)
    {
        // ---- Common emoji ranges ----
        if ((cp >= 0x1F300 && cp <= 0x1F5FF) || // Misc Symbols & Pictographs
            (cp >= 0x1F600 && cp <= 0x1F64F) || // Emoticons
            (cp >= 0x1F680 && cp <= 0x1F6FF) || // Transport & Map
            (cp >= 0x1F700 && cp <= 0x1F77F) || // Alchemical Symbols (some with emoji style)
            (cp >= 0x1F780 && cp <= 0x1F7FF) || // Geometric Shapes Extended
            (cp >= 0x1F800 && cp <= 0x1F8FF) || // Supplemental Arrows-C (some emoji)
            (cp >= 0x1F900 && cp <= 0x1F9FF) || // Supplemental Symbols & Pictographs
            (cp >= 0x1FA70 && cp <= 0x1FAFF) || // Symbols & Pictographs Extended-A
            (cp >= 0x2600 && cp <= 0x26FF) || // Misc Symbols
            (cp >= 0x2700 && cp <= 0x27BF) || // Dingbats
            (cp >= 0x1F1E6 && cp <= 0x1F1FF)) // Regional Indicators (flags)
        {
            return true;
        }

        // ---- Special control characters ----
        if (cp == 0x200D || // Zero Width Joiner (ZWJ)
            cp == 0xFE0F) // Variation Selector-16 (emoji presentation)
        {
            return true;
        }

        // ---- Fallback: rely on Unicode category ----
        // This catches some emoji-like symbols such as Misc Symbols/Dingbats.
        var uc = CharUnicodeInfo.GetUnicodeCategory(char.ConvertFromUtf32(cp), 0);
        if (uc == UnicodeCategory.OtherSymbol)
        {
            return true;
        }

        return false;
    }
}