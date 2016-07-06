import java.awt.Point;
import java.util.Scanner;
import java.util.ArrayList;
import java.util.LinkedList;

class CodeIqTraceRoute {

	public static void main(String[] args) {

        Scanner cin=new Scanner(System.in);
        String line;
        for(;cin.hasNext();){
            line=cin.nextLine();
            CodeIqTraceRoute tr = new CodeIqTraceRoute();
			if (tr.validInput(line.toUpperCase())) {
				// マップ作成
				tr.createMap(line.toUpperCase());
				// 全ルート数カウント
				System.out.println(tr.countAllRoute(line.toUpperCase()));
			}
        }

	}

	private final int MAX_X = 37;
	private final int MAX_Y = 37;
	private final int CENTER_X = 18;
	private final int CENTER_Y = 18;
	private final String MOVE_PATTERN = "UDRL";

	private int[][] field = new int[MAX_X][MAX_Y];


	/**
	 * 入力値を検証する
	 *
	 * @param input 入力値
	 * @return true 検証OK   false 検証NG
	 */
	private boolean validInput(String input) {
		if (input.length() > 18) {
			System.out.println("入力文字列は18文字以内です。");
			return false;
		}
		if (input.matches(String.format("[^UDRL]+"))) {
			System.out.println("UDRL以外の文字は入力できません。");
			return false;
		}
		return true;
	}

	/**
	 * マップ移動ルートに沿ったマップを作成する
	 *
	 * @param input マップ移動ルート
	 */
	private void createMap(String input) {
		// 中央をスタート位置に設定
		Route cur = new Route(CENTER_X, CENTER_Y);
		field[cur.x][cur.y] = 1;
		// 移動ルートにマップのフラグを立てる
		for (char ch : input.toCharArray()) {
			cur.move(ch);
			field[cur.x][cur.y] = 1;
		}
	}

	/**
	 * 全ルート数をカウントする
	 * 全通りのスタート位置からのルート数をもとめて合算する
	 *
	 * @param input マップ移動ルート
	 * @return 全ルート数
	 */
	private long countAllRoute(String input) {
		long routeCount = 0;
		for (int i = 0; i < MAX_X; i++) {
			for (int j = 0; j < MAX_Y; j++) {
				if (field[i][j] == 1) {
					routeCount += this.countRoute(new Route(i, j), input);
				}
			}
		}
		return routeCount;
	}

	/**
	 * 指定スタート位置からのルート数をもとめる
	 *
	 * @param start スタート位置
	 * @param input マップ移動ルート
	 * @return
	 */
	private long countRoute(Route start, String input) {

		// 現在値までのキュー
		LinkedList<Route> routes = new LinkedList<Route>();
		// 行き止まりまでのルート
		ArrayList<String> route = new ArrayList<String>();

		// スタート位置をキューへ格納
		routes.push(start);

		while (!routes.isEmpty()) {
			// キューの先頭を取り出し
			Route cur = routes.pop();

			// 全移動パターンで移動
			for (char ch : MOVE_PATTERN.toCharArray()) {
				if (cur.isMove(ch)) {
					// 移動可能(マップ範囲内)の場合に現在値から移動してキューへ格納
					Route next = cur.copyRoute();
					next.move(ch);
					routes.push(next);
				} else {
					// 行き止まりとなった場合、移動履歴をルートとして格納
					if (cur.getMoveHist().length() == input.length() && !route.contains(cur.getMoveHist())) route.add(cur.getMoveHist());
				}
			}
		}

		// マップ移動ルートと移動履歴数のルート数を返す
		return route.stream()
						.filter(hist -> hist.length() == input.length())
						.count();
	}


	/**
	 * 移動経路の現在値や移動履歴、訪問履歴を管理する内部クラス
	 */
	protected class Route extends Point {
		protected String moveHist = "";
		private int [][] visited = new int[MAX_X][MAX_Y];
		private int initX = 0;
		private int initY = 0;

		/**
		 * 移動履歴を取得
		 *
		 * @return 移動履歴
		 */
		public String getMoveHist() {
			return this.moveHist;
		}

		/**
		 * コンストラクタ
		 *
		 * @param x
		 * @param y
		 */
		protected Route(int x, int y) {
			super (x, y);
			this.visited[x][y] = 1;
			this.initX = x;
			this.initY = y;
		}

		/**
		 * 現在値がマップ内か判定
		 *
		 * @param x
		 * @param y
		 * @return true マップ内   false マップ外
		 */
		private boolean isInRange(int x, int y) {
			return (0 <= x && x <= MAX_X - 1) && (0 <= y && y <= MAX_Y - 1)
					&& (field[x][y] == 1);
		}

		/**
		 * 現在地から移動したポイントを返す
		 *
		 * @param x
		 * @param y
		 * @param direction 移動方向
		 * @return
		 */
		private Point getMovePoint(int x, int y, char direction) {
			Point p = new Point(x, y);
			switch (direction) {
				case 'U':
					p.translate(0, 1);
					break;
				case 'D':
					p.translate(0, -1);
					break;
				case 'R':
					p.translate(1, 0);
					break;
				case 'L':
					p.translate(-1, 0);
					break;
				default:
			}
			return p;
		}

		/**
		 * 移動可能か判定
		 *
		 * @param direction 移動方向
		 * @return true 移動可能   false 移動不可
		 */
		protected boolean isMove(char direction) {
			Point p = this.getMovePoint(this.x, this.y, direction);
			return this.isInRange(p.x, p.y) && this.visited[p.x][p.y] == 0;
		}

		/**
		 * 現在値を移動
		 *
		 * @param direction 移動方向
		 */
		protected void move(char direction) {
			Point p = this.getMovePoint(this.x, this.y, direction);
			this.move(p.x, p.y);
			this.moveHist += direction;
			this.visited[this.x][this.y] = 1;
		}


		/**
		 * 本インスタンスをディープコピー
		 *
		 * @return コピー
		 */
		protected Route copyRoute() {
			Route route = new Route(this.initX, this.initY);
			for (char ch : this.moveHist.toCharArray()) {
				route.move(ch);
			}
			return route;
		}
	}

}
