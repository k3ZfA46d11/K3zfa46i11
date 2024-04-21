using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace ContentWarningK3zfa46i11
{
    internal class CheckUpdate
    {
        private static bool hasNewUpdate = false;
        public const string Version = "v1.0";

        public static void DrawHasNewUpdate()
        {
            if (!hasNewUpdate)
                return;
            Renderer.DrawColorString(new Vector2(Screen.width / 2, 20f), "K3zfa46i11有新的版本可用！", Color.yellow, 20f);
        }

        public static async void CheckForUpdate()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Xiaodo-APP");
                client.Timeout = TimeSpan.FromSeconds(20);
                HttpResponseMessage response = await client.GetAsync("https://api.github.com/repos/k3ZfA46d11/K3zfa46i11/releases/latest");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log($"[K3zfa46i11] 从GitHub存储库获取最新版本成功.");
                    JObject json = JObject.Parse(responseBody);
                    hasNewUpdate = (json["tag_name"].ToString() != Version);
                    if(hasNewUpdate) Win32.ShellExecuteA(IntPtr.Zero, new StringBuilder("open"), new StringBuilder($@"https://github.com/k3ZfA46d11/K3zfa46i11/releases/tag/{json["tag_name"].ToString()}"), new StringBuilder(), new StringBuilder(), 0);
                }
                else
                {
                    Debug.Log($"[K3zfa46i11] 从GitHub存储库获取最新版本失败.错误代码{response.StatusCode}");
                    hasNewUpdate = false;
                }
            }
        }
    }
}
