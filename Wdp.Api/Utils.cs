using System.Net;
using System.Text;

namespace Wdp.Api
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Utils
    {
        public static async Task<string> GetWebsiteIconUrlAsync(string url)
        {
            return await Task.Run(() =>
            {
                string iconUrl = "";
                try
                {
                    WebRequest wrequest = WebRequest.Create(url);
                    WebResponse wresponse = wrequest.GetResponse();
                    using (Stream resStream = wresponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(resStream, Encoding.Default))
                        {
                            string html = sr.ReadToEnd();
                            /*
                             * <meta name="description" content="全球领先的中文搜索引擎、致力于让网民更便捷地获取信息，找到所求。百度超过千亿的中文网页数据库，可以瞬间找到相关的搜索结果。">
                             * <link rel="shortcut icon" href="//www.baidu.com/favicon.ico" type="image/x-icon">
                             * <link rel="search" type="application/opensearchdescription+xml" href="//www.baidu.com/content-search.xml" title="百度搜索">
                             * 
                             * */

                            //截取head部分
                            int startIndex = html.IndexOf("<head>");
                            int endIndex = html.IndexOf("</head>");
                            string header = html.Substring(startIndex + 6, endIndex - startIndex);

                            //定位icon所在的link标签

                            int tempIndex = header.IndexOf("image/x-icon");
                            if(tempIndex == -1)
                            {
                                tempIndex = header.IndexOf("image/svg+xml");
                            }

                            if (tempIndex == -1)
                            {
                                tempIndex = header.IndexOf("icon");
                            }


                            if (tempIndex == -1) return "";
                            string tempStartStr = header.Substring(0, tempIndex);
                            string tempEndStr = header.Substring(tempIndex);

                            startIndex = tempStartStr.LastIndexOf("<");
                            endIndex = tempEndStr.IndexOf(">");

                            string link = tempStartStr.Substring(startIndex) + tempEndStr.Substring(0, endIndex);

                            //定位href属性

                            startIndex = link.IndexOf("href=") + 6;

                            string href = link.Substring(startIndex);

                            iconUrl = href.Substring(0, href.IndexOf("\""));

                            if (!iconUrl.Contains("http") && !iconUrl.Contains("//"))
                            {
                                startIndex = url.IndexOf("://") + 3;
                                tempEndStr = url.Substring(startIndex);
                                if (tempEndStr.Contains("/"))
                                {
                                    tempEndStr = tempEndStr.Substring(0, tempEndStr.IndexOf("/"));
                                }
                                iconUrl = url.Substring(0, startIndex + tempEndStr.Length) + iconUrl;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    iconUrl = "";
                }
                return iconUrl;

            });

        }
    }
}
