import React from "react";
import { render, fireEvent, cleanup } from "@testing-library/react";
import VS from "../components/pages/friends/ListFriends.js";
afterEach(cleanup);

it("renders without crashing", () => {
	const { queryByTestId } = render(<VS />);
	expect(queryByTestId("lineTest")).toBeTruthy();
	expect(queryByTestId("createLineTest")).toBeTruthy();
});

describe("Input of VS date", () => {
	it("Updates based on input", () => {
		const { queryByTestId } = render(<VS />);
		const codeInput = queryByTestId("lineIDTest");
		fireEvent.change(codeInput, { target: { value: "" } });
		expect(codeInput.value).toBe("");
	});
});
