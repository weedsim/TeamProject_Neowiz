using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class EncodingPostProcessor : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string path in importedAssets)
        {
            // .cs 파일만 대상
            if (!path.EndsWith(".cs")) continue;

            // 프로젝트 내의 절대 경로 구하기
            string systemPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, path);

            if (!File.Exists(systemPath)) continue;

            // [중요] 이미 UTF-8 BOM인지 확인하여 무한 루프 방지
            if (IsAlreadyUtf8BOM(systemPath)) continue;

            // [중요] 유니티의 임포트 프로세스가 끝난 뒤(다음 프레임)에 실행하도록 예약
            // 이렇게 해야 "Build asset version error"가 발생하지 않음
            string capturedPath = path; // 람다 캡처용 변수
            string capturedSystemPath = systemPath;

            EditorApplication.delayCall += () =>
            {
                // 파일이 여전히 존재하는지 확인
                if (!File.Exists(capturedSystemPath)) return;

                // 1. 기존 내용을 읽음 (한글 깨짐 방지를 위해 EUC-KR/949로 시도하거나 시스템 기본값 사용)
                // 유니티 6에서 949로 생성되었다면 Encoding.Default(한글 윈도우) 혹은 949로 읽어야 함
                string content = "";
                try
                {
                    // CP949로 읽기 시도
                    content = File.ReadAllText(capturedSystemPath, Encoding.GetEncoding(949));
                }
                catch
                {
                    // 실패 시 기본값으로 읽기
                    content = File.ReadAllText(capturedSystemPath);
                }

                // 2. UTF-8 with BOM으로 덮어쓰기
                File.WriteAllText(capturedSystemPath, content, new UTF8Encoding(true));

                // 3. 변경 사항을 유니티에 알림 (재임포트 트리거)
                AssetDatabase.ImportAsset(capturedPath);

                Debug.Log($"[EncodingFixer] Converted to UTF-8 BOM: {capturedPath}");
            };
        }
    }

    // 파일 헤더(BOM)를 직접 읽어서 UTF-8 식별자가 있는지 확인하는 함수
    private static bool IsAlreadyUtf8BOM(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            if (fs.Length < 3) return false;

            byte[] bom = new byte[3];
            fs.Read(bom, 0, 3);

            // UTF-8 BOM 바이트: 0xEF, 0xBB, 0xBF
            return bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF;
        }
    }
}