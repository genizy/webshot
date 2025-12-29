import { Component, createDelegate, css } from "dreamland/core";
import { GameView } from "./game";
import { StickyNote } from "./splash";

let App: Component<{}, { showSplash: boolean }> = function () {
	let preinit = createDelegate<void>();

	this.showSplash = false;
	preinit();

	return (
		<div id="app">
			{use(this.showSplash).andThen(
				<div class="splash">
					<StickyNote done={() => this.showSplash = false} />
				</div>
			)}
			<GameView preinit={preinit} showSplash={use(this.showSplash)} />
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

	.splash {
		position: absolute;
		z-index: 100;
		inset: 0;

		backdrop-filter: blur(10px);

		display: flex;
		align-items: center;
		justify-content: center;
	}
`;

document.querySelector("#app")?.replaceWith(<App />);
