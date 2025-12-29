import { Component, css } from "dreamland/core";
import { settings } from "./store";

let MONOMOD_WASM = "https://github.com/r58Playz/MonoMod";
let GITHUB = "https://github.com/MercuryWorkshop/webshot"

export let StickyNoteMinimal: Component = function() {
	return (
		<div>
			<div>ONESHOT: WORLD MACHINE EDITION</div>
			<div>Machine Owned By: {use(settings.name)}</div>
		</div>
	)
}
StickyNoteMinimal.style = css`
	:scope {
		background: var(--sticky-note);
		color: #000;
		width: 16rem;
		height: 100%;
		overflow: hidden;
		
		padding: 0.5rem 0;
		font-size: 1rem;
		text-align: center;
	}
`;

export let StickyNote: Component<{ done: () => void }> = function () {
	return (
		<div>
			<div class="headline">ONESHOT: WORLD MACHINE EDITION</div>
			<p>
				This is a port of OneShot: World Machine Edition to the browser using dotnet and FNA's threaded WebAssembly support.
				It also technically supports hotpatching the game through <a href={MONOMOD_WASM} target="_blank">MonoMod.WASM</a> but there's no modloader yet!
			</p>
			<p>
				You will need to own the game and have it downloaded to play this port. The source is available on <a href={GITHUB} target="_blank">GitHub</a>.
			</p>
			<p>
				Machine Owned By: {use(settings.name)}
			</p>
			<div class="exit"><button on:click={this.done}><span>[</span> CLOSE <span>]</span></button></div>
		</div>
	)
}
StickyNote.style = css`
	:scope {
		background: var(--sticky-note);
		color: #000;
		width: 30rem;
		aspect-ratio: 1 / 1;
		overflow: hidden;
		
		padding: 2rem;
		font-size: 1.25rem;
	}

	.headline {
		font-size: 1.6rem;
		text-align: center;
		margin-bottom: 2.5rem;
	}

	input, button {
		background: none;
		outline: none;
		border: none;
		padding: 0 0.25rem;
		font-size: 1.25rem;
	}

	input {
		border-bottom: 2px solid #000;
	}

	.exit {
		display: flex;
		justify-content: center;
	}

	button {
		font-size: 1.4rem;
	}
	button span {
		visibility: hidden;
	}
	button:hover span {
		visibility: visible;
	}
`;
