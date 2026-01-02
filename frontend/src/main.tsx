import { Component, createDelegate, css } from "dreamland/core";
import { GameView } from "./game";
import "./index.css"

let App: Component = function () {
	let preinit = createDelegate<void>();

	// Defer preinit() to after mount so GameView can attach its listener
	setTimeout(() => preinit(), 0);

	return (
		<div id="app">
			<GameView preinit={preinit} />
		</div>
	)
}
App.style = css`
	:scope {
		width: 100%;
		height: 100%;

		position: relative;
		overflow: hidden;
	}
`;

document.querySelector("#app")?.replaceWith(<App />);
