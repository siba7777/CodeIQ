package codeiq.countbutton;

import java.util.Scanner;

class CodeIqCountButton {


	public static void main(String[] args) {
		try (Scanner cin=new Scanner(System.in)) {
			String line = "";
			for(;cin.hasNext();){
				line=cin.nextLine();
				System.out.println(CountButtonClick(Long.parseLong(line)));
	        }
		}
	}

	/**
	 * 入力値 ⇒ 「1」とするために、
	 * ボタン「×2」or ボタン「＋1」を押した最小回数をカウント
	 *
	 * @param inputValue 入力値
	 * @return ボタンを押した回数
	 */
	public static int CountButtonClick(long inputValue) {
		int counter = 0;
		long outputValue = inputValue;
		while (1 != outputValue) {
			if (outputValue % 2 == 0) {
				// 偶数の場合は ÷2
				outputValue /= 2;
				counter++;
			} else {
				// 奇数の場合は －1
				outputValue -= 1;
				counter++;
			}
		}
		return counter;
	}

}
