"""----------------------------------------------------------------------------------------"""
"""AOE2DEリプレイファイル解析テストメイン"""
"""カレントファイル上でないとデバッガーがおかしくなるので__init__ファイルに追記"""
"""----------------------------------------------------------------------------------------"""

import os, time, sys
from mgz import header, fast
from mgz.model import parse_match

"""デフォルトファイルパス（引数指定で入れ替え）"""
replayfilepath = 'C:/Users/yoshi/Games/Age of Empires 2 DE/76561199387650333/savegame/MP Replay v101.102.9814.0 #(75350) @2023.02.24 202914 (1).aoe2record'
outfilepath = ''
"""処理中のファイルポインタ"""
seekcurrent = 0

"""引数でファイル指定があれば、そのファイルフルパスに入れ替え"""
args = sys.argv

"""リプレイファイルパス"""
if 1 < len(args):
    replayfilepath = args[1]

"""出力ファイルパス"""
if 2 < len(args):
    outfilepath = args[2]
    if os.path.isfile(outfilepath):
        os.remove(outfilepath)

outfilepath = "map.txt"
if os.path.isfile(outfilepath):
    os.remove(outfilepath)

import json
from mgz.model import parse_match, serialize

"""ヘッダ情報ファイル出力"""

print("HEADERSTART")

with open(replayfilepath, 'rb') as data:
    match = parse_match(data)
    seekcurrent = data.tell()

    if(outfilepath != ''):
        """print("TEAMSSTART")
        print(json.dumps(serialize(match.teams), indent=2))
        print("TEAMSEND")"""

        for teamno, team in enumerate(match.teams):
            for item in team:
                playername = item.name.encode('unicode-escape').decode('utf-8')
                wrkStr = "TEAMS;" + str(item.number) + ";" + str(teamno + 1) + ";" + str(item.color_id) + ";" + item.color + ";" + str(item.civilization_id) + ";" + item.civilization + ";" + str(item.position.x) + ";" + str(item.position.y) + ";" + str(playername)
                print(wrkStr)

        wrkStr = "MAP;" + match.map.name + ";" + match.map.size + ";" + str(match.map.dimension) + ";" + str(match.map.seed)
        print(wrkStr)

        """print("MAPSSTART")
        with open(outfilepath, 'a') as f:
            print(json.dumps(serialize(match.map), indent=2), file=f)
            print(json.dumps(serialize(match.gaia), indent=2), file=f)
        print("MAPSEND")"""

print("HEADEREND")

"""ヘッダー部処理（起動後初回のみ）"""
with open(replayfilepath, 'rb') as data:
    eof = os.fstat(data.fileno()).st_size
    head = header.parse_stream(data)
    met = fast.meta(data)
    seekcurrent = data.tell()

wrkOldViewStr = ""
wrkOldSyncStr = ""
wrkOldChatStr = ""
wrkOldPostStr = ""

"""ボディ部処理（ループで毎回差分のみ）"""
while True:
    with open(replayfilepath, 'rb') as data:

        eof = os.fstat(data.fileno()).st_size

        """この回の差分を格納する文字バッファ"""
        wrkStr = ""

        """前回の続きから継続させるため位置合わせ"""
        data.seek(seekcurrent)

        """差分処理用ループ"""
        while data.tell() < eof:

            """変更有フラグ"""
            modflg = False

            """各データ処理"""
            try:
                rtn = fast.operation(data)
            except ValueError:
                continue

            """オペレーション種別"""
            op_type = rtn[0]

            """アクション系"""
            if op_type == fast.Operation.ACTION:
                wrkStr = "ACTION;" + str(rtn[1][0]) + ";" + str(rtn[1][1])
                modflg = True

            """同期系"""
            """if op_type == fast.Operation.SYNC:
                wrkSyncStr = "SYNC;" + str(rtn)
                if wrkOldSyncStr != wrkSyncStr:
                    wrkStr = wrkSyncStr
                    wrkOldSyncStr = wrkSyncStr
                    modflg = True"""

            """オーナー視点"""
            if op_type == fast.Operation.VIEWLOCK:
                wrkViewStr = "VIEWLOCK;" + str(rtn[1][0]) + ";" + str(rtn[1][1])
                if wrkOldViewStr != wrkViewStr:
                    wrkStr = wrkViewStr
                    wrkOldViewStr = wrkViewStr
                    modflg = True

            """チャット"""
            if op_type == fast.Operation.CHAT:
                wrkChatStr = "CHAT;" + str(rtn[1])
                if wrkOldChatStr != wrkChatStr:
                    wrkStr = wrkChatStr
                    wrkOldChatStr = wrkChatStr
                    modflg = True

            """ゲーム投了"""
            if op_type == fast.Operation.POSTGAME:
                wrkPostStr = "POSTGAME;" + str(rtn)
                if wrkOldPostStr != wrkPostStr:
                    wrkStr = wrkPostStr
                    wrkOldPostStr = wrkPostStr
                    modflg = True

            """差分があれば標準出力へ文字出力"""
            if(modflg):
                print(wrkStr)
                sys.stdout.flush()

                """ファイル出力"""
                """if(outfilepath != ''):
                    with open(outfilepath, 'a') as f:
                        print(wrkStr, file=f)"""
            """time.sleep(0.0005)"""

        """今回の終了時のファイル位置を記録"""
        seekcurrent = eof

        """1秒おきに差分チェックループ"""
        time.sleep(1)
