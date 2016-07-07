package codeiq.calcfare;

import java.util.ArrayList;
import java.util.Scanner;

class CodeIqCalcFare {

	private int adult = 0;
	private int child = 0;
	private int infant = 0;
	private ArrayList<Double> fareList = new ArrayList<Double>();

	public static void main(String[] args) {
		try (Scanner cin=new Scanner(System.in)) {
			String line;
			for(;cin.hasNext();){
				line=cin.nextLine();
				CodeIqCalcFare cf = new CodeIqCalcFare();
				cf.parseInput(line.toUpperCase());
				System.out.println(cf.calcFare());
	        }
		}
	}

	/**
	 * 入力値から、バス料金と、乗客の区分毎の人数を抽出
	 *
	 * @param input 入力値
	 */
	protected void parseInput(String input) {
		// 料金と乗客を分ける
		String[] inputs = input.split(":");
		if (inputs.length < 2) return;
		// 区間毎のバス料金を格納
		for (String fare : inputs[0].split(",")) {
			if (fare.isEmpty()) continue;
			this.fareList.add(Double.parseDouble(fare.trim()));
		}
		// 乗客の区分毎に人数をカウント
		for (String passenger : inputs[1].split(",")) {
			if (passenger.isEmpty()) continue;
			switch (passenger.trim()) {
			case "A":
				this.adult++;
				break;
			case "C":
				this.child++;
				break;
			case "I":
				this.infant++;
				break;
			}
		}
	}

	/**
	 * 合計料金を計算
	 *
	 * @return 合計料金
	 */
	protected int calcFare() {
		int totalFare = 0;
		for (Double fare : this.fareList) {
			totalFare += fare * adult;
			totalFare += Math.ceil(fare / 2 / 10) * 10 * child;
			int payNum = infant > adult * 2 ? infant - adult * 2 : 0;
			totalFare += Math.ceil(fare / 2 / 10) * 10 * payNum;
		}
		return totalFare;
	}

}
