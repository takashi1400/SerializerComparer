using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using DiffMatchPatch;
using System.Linq;

namespace SerializerComparer
{
    /// <summary>
    /// Compare Options.
    /// </summary>
    public class SerializerComparerOptions
    {
        /// <summary>
        /// whether output as html or not.
        /// HTML形式で出力するかどうか
        /// </summary>
        public bool AsHtml { get; set; }

        /// <summary>
        /// specifies where to output.
        /// 出力ファイル先の指定。
        /// 指定がなければコンソールに出力します。
        /// </summary>
        public string FilePath { get; set; }

    };

    /// <summary>
    /// compare any type of object and output as json.
    /// ある特定のデータ型をJson形式で比較します。
    /// </summary>
    /// <typeparam name="T">特定のデータ型</typeparam>
    public static class SerializerComparer<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataA">object A to be compared</param>
        /// <param name="dataB">object B to be compared</param>
        /// <param name="options">option</param>
        /// <param name="comparerOptions">comparer Options</param>
        /// <returns></returns>
        public static string Compare(T dataA, T dataB, JsonSerializerOptions options = null, SerializerComparerOptions comparerOptions = null)
        {

            string jsonStringA = JsonSerializer.Serialize<T>(dataA, options);
            string jsonStringB = JsonSerializer.Serialize<T>(dataB, options);

            diff_match_patch dmp = new diff_match_patch();

            List<Diff> diff = dmp.diff_main(jsonStringA, jsonStringB);

            dmp.diff_cleanupSemantic(diff);

            string result = "";

            if (comparerOptions?.AsHtml ?? false)
            {
                result = dmp.diff_prettyHtml(diff);
            }
            else
            {
                result = string.Join(",", diff.Select(e=>e.ToString()).ToList());
            }

            //if the file is empty, write to console.
            if (string.IsNullOrEmpty(comparerOptions?.FilePath))
            {
                Console.WriteLine(result);
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(comparerOptions.FilePath))
                {
                    sw.Write(result);
                }
            }

            return result;
        }
    }
}
