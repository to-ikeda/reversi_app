using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public string player = "black";


    //ここをボードのスクリプトに組み込む
    //static void Show(Reversi r)
    //{

    //    Debug.Log(" ");
    //    for(int i = 0 ; i < Reversi.N ; i++ )
    //        Debug.Log(i);
    //    Debug.Log("---");

    //    for(int i = 0 ; i < Reversi.N ; i++ )
    //    {
    //        Debug.Log(i);
    //        for(int j = 0 ; j < Reversi.N ; j++ )
    //        {
    //            switch( r.Board[j,i] )
    //            {
    //                case Reversi.BLACK:
    //                    Debug.Log("●");
    //                    break;
    //                case Reversi.WHITE:
    //                    Debug.Log("○");
    //                    break;
    //                default:
    //                    Debug.Log("　");
    //                    break;
    //            }
    //        }
    //        Debug.Log("---");
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {

        //ここをボードのスクリプトに組み込む
        ////// ゲーム用クラスを用意
        ////var r = new Reversi();
        ////int x, y;

        ////while (r.CheckFinish() == false)
        ////{
        ////    Show(r);

        ////    if (r.Turn)
        ////    {
        ////        // 人間からの入力
        ////        Debug.Log("左からの番号を入力");
        ////        if (int.TryParse(Debug.Log(), out x) == false) continue;
        ////        Debug.Log("上からの番号を入力");
        ////        if (int.TryParse(Console.ReadLine(), out y) == false) continue;
        ////        r.PutStone(x, y);
        ////    }
        ////    else
        ////    {
        ////        // AIに打たせる
        ////        r.AIPut();
        ////    }
        ////}

    }

    // Update is called once per frame
    void Update()
    {
        
    }



}

class Reversi
{
    /// <summary>
    /// 状態を保存するボード
    /// </summary>
    public int[,] Board;
    /// <summary>
    /// 一辺のマスの数
    /// </summary>
    public const int N = 8;
    /// <summary>
    /// 何もない状態
    /// </summary>
    public const int NONE = 0;
    /// <summary>
    /// 白の石
    /// </summary>
    public const int WHITE = 1;
    /// <summary>
    /// 黒の石
    /// </summary>
    public const int BLACK = -1;
    /// <summary>
    /// どちらの順番がを示す変数(trueなら黒)
    /// </summary>
    public bool Turn;
    /// <summary>
    /// ボードの状態を保存する変数
    /// </summary>
    private List<int[,]> BoardHistory;
    /// <summary>
    /// 手番を保存する変数
    /// </summary>
    private List<bool> TurnHistory;
    /// <summary>
    /// デフォルトの評価ボード
    /// </summary>
    private int[,] EvaluationBoard = new int[,]{
                {  60, -25, 15, 15, 15, 15,-25, 60},
                { -25, -50,-30,-30,-30,-30,-50,-25},
                {  15, -30, 15, 15, 15, 15,-30, 15},
                {  15, -30, 15, 25, 25, 15,-30, 15},
                {  15, -30, 15, 25, 25, 15,-30, 15},
                {  15, -30, 15, 15, 15, 15,-30, 15},
                { -25, -50,-30,-30,-30,-30,-50,-25},
                {  60, -25, 15, 15, 15, 15,-25, 60}
    };

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Reversi()
    {
        this.Init();
    }

    /// <summary>
    /// 初期化メソッド
    /// </summary>
    public void Init()
    {
        this.Board = new int[N, N];
        this.Board[3, 3] = WHITE;
        this.Board[4, 4] = WHITE;
        this.Board[3, 4] = BLACK;
        this.Board[4, 3] = BLACK;
        this.Turn = true;

        this.BoardHistory = new List<int[,]>();
        this.TurnHistory = new List<bool>();
    }

    /// <summary>
    /// 置ける場所かどうかを判定するメソッド
    /// </summary>
    /// <param name="x">判定するx座標</param>
    /// <param name="y">判定するy座標</param>
    /// <returns>置ける場合はtrue</returns>
    public bool CanPut(int x, int y)
    {
        //とりあえず実際に置いてみる
        var ret = Put(x, y);

        //おけなかったらおけない場所(当然)
        if (ret == false)
            return false;

        //勝手に置くのはダメなので元に戻す
        Undo();

        return true;
    }

    /// <summary>
    /// 元に戻すメソッド
    /// </summary>
    private void Undo()
    {
        // 一番最後の要素のindex
        int n = this.BoardHistory.Count - 1;

        if (n < 0)
            return;

        // 一個前の状態に戻す
        this.Board = this.BoardHistory[n];
        this.Turn = this.TurnHistory[n];

        // その時のボードの状態・手番は消す
        this.BoardHistory.RemoveAt(n);
        this.TurnHistory.RemoveAt(n);
    }

    /// <summary>
    /// 手番を変更するメソッド
    /// </summary>
    private void ChangeTurn()
    {
        // とりあえず順番変える
        this.Turn = !this.Turn;

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                // おける場所が一か所でもあればOK
                if (CanPut(i, j) == true)
                    return;
            }
        }

        // おける場所がなかったので手番は元に戻る
        this.Turn = !this.Turn;
    }

    /// <summary>
    /// ゲームが終了したかどうかを判定するメソッド
    /// </summary>
    /// <returns>終了したらtrue</returns>
    public bool CheckFinish()
    {
        // ChangeTurnで!Turnの場合は置ける場所がないのはわかっている
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (CanPut(i, j) == true)
                    return false;
            }
        }

        // 今の人も置く場所がないので終わり
        return true;
    }

    /// <summary>
    /// 石の数を数え上げるメソッド
    /// </summary>
    /// <param name="target">数える対象</param>
    /// <returns>石の数</returns>
    public int CountStone(int target)
    {
        int count = 0;
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                if (Board[i, j] == target)
                    count++;

        return count;
    }

    /// <summary>
    /// 石を置き，手番を変更するメソッド
    /// </summary>
    /// <param name="x">置く場所のx座標</param>
    /// <param name="y">置く場所のy座標</param>
    /// <returns>置くことが出来たらtrueを返す</returns>
    public bool PutStone(int x, int y)
    {
        // とりあえず置く
        var flag = Put(x, y);

        if (flag == false)
            return false;

        ChangeTurn();

        return true;
    }

    /// <summary>
    /// 石を置くメソッド
    /// </summary>
    /// <param name="x">置く場所のx座標</param>
    /// <param name="y">置く場所のy座標</param>
    /// <returns>置くことが出来たらtrueを返す</returns>
    private bool Put(int x, int y)
    {
        //範囲外なら何もせず返す
        if (InRange(x, y) == false)
            return false;
        // なにかあったら置けない
        if (Board[x, y] != NONE)
            return false;

        // ひっくり返したかどうかを格納するメソッド
        bool isChanged = false;
        // 現在の状態を一旦保存する
        var currentBoard = (int[,])(this.Board.Clone());
        // 現在の攻撃側はどちらかを一旦保存する
        var currentTurn = this.Turn;

        for (int i = 0; i < 9; i++)
        {
            //これでdx,dyは-1から1までの値が入る
            int dx = i / 3 - 1;
            int dy = i % 3 - 1;

            //両方共0じゃなければ
            //(dx,dy)方向へひっくり返せるかを調べる
            if (dx != 0 || dy != 0)
                isChanged = isChanged | Reverse(x, y, dx, dy);
        }

        // ひっくり返さなかった場合はfalse
        if (isChanged == false)
            return false;


        // ココに来てやっと置くことが出来る
        this.Board[x, y] = NowStone();

        // 手番とボードの状態を保存
        this.BoardHistory.Add(currentBoard);
        this.TurnHistory.Add(currentTurn);

        return true;
    }

    /// <summary>
    /// 石をひっくり返すメソッド
    /// </summary>
    /// <param name="x">石をおいたx座標</param>
    /// <param name="y">石をおいたy座標</param>
    /// <param name="dx">調べる方向のx</param>
    /// <param name="dy">調べる方向のy</param>
    /// <returns>ひっくり返せたらtrue</returns>
    private bool Reverse(int x, int y, int dx, int dy)
    {
        var attack = NowStone();
        var defense = -attack;

        // その方向が枠の外ならひっくり返せない
        if (InRange(x + dx, y + dy) == false)
            return false;
        // 一個先を見て敵の石じゃなかったらひっくり返せない
        if (Board[x + dx, y + dy] != defense)
            return false;


        // その先を見ていく
        for (int i = 2; i < N; i++)
        {
            int index_x = x + i * dx;
            int index_y = y + i * dy;

            if (InRange(index_x, index_y) == false)
            {
                //範囲外ならfalse
                return false;
            }
            else if (Board[index_x, index_y] == attack)
            {
                //探した先に攻撃側の駒があった場合はひっくり返す
                for (; i >= 1; i--)
                    Board[x + i * dx, y + i * dy] = attack;
                return true;
            }
            else if (Board[index_x, index_y] == NONE)
            {
                // その先に仲間の石がなかったのでfalse
                return false;
            }
        }

        // 仲間の石が見つからなかったのでfalse
        return false;
    }

    /// <summary>
    /// 現在攻撃側の石を置くメソッド
    /// </summary>
    /// <returns></returns>
    private int NowStone()
    {
        if (this.Turn)
            return BLACK;
        else
            return WHITE;
    }

    /// <summary>
    /// 特定の位置が配列の範囲内かどうかを判定するメソッド
    /// </summary>
    /// <param name="x">判定するx座標</param>
    /// <param name="y">判定するy座標</param>
    /// <returns>範囲内ならtrue</returns>
    private bool InRange(int x, int y)
    {
        if (x < 0 || x >= N)
            return false;
        if (y < 0 || y >= N)
            return false;

        return true;
    }

    /// <summary>
    /// 評価関数
    /// </summary>
    /// <param name="evaluationBoard">評価ボード</param>
    /// <returns>評価値(黒が有利なら正)</returns>
    private int Evaluate(int[,] evaluationBoard)
    {
        var point = 0;

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                point += evaluationBoard[i, j] * Board[i, j];
            }
        }

        return point;
    }

    /// <summary>
    /// 人工知能による最適な手の選択し，置く
    /// </summary>
    /// <param name="evaluationBoard">評価ボード</param>
    public void AIPut(int[,] evaluationBoard)
    {
        int max = int.MinValue;
        int x = 0;
        int y = 0;
        // 自分の石を覚えとく
        int myStone = NowStone();

        // 終わっていたら関係ない
        if (CheckFinish() == true)
            return;

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                // とりあえず石を置く
                if (PutStone(i, j) == true)
                {
                    // 置けたら盤面評価
                    // 自分の石の値をかけて常に正にする
                    var point = myStone * Evaluate(evaluationBoard);

                    // ポイントが高かったら
                    if (point > max)
                    {
                        // その手を保存する
                        x = i;
                        y = j;
                        max = point;
                    }

                    // 元に戻す
                    Undo();
                }
            }
        }

        // 置ける場所なら置く
        if (InRange(x, y) == true)
            PutStone(x, y);
    }

    /// <summary>
    /// 人工知能による最適な手の選択し，置く
    /// </summary>
    public void AIPut()
    {
        AIPut(this.EvaluationBoard);
    }
}
