import { useEffect, useState } from "react";
import {
	getStatus,
	type StatusResponse,
} from "../api/statusApi";

type LoadState =
	| { kind: "loading" }
	| { kind: "loaded"; status: StatusResponse }
	| { kind: "error"; message: string };

export function StatusPage() {
	const [state, setState] =
		useState<LoadState>({ kind: "loading" });

	useEffect(() => {
		let active = true;

		getStatus()
			.then((status) => {
				if (active) {
					setState({ kind: "loaded", status });
				}
			})
			.catch((error: unknown) => {
				if (!active) {
					return;
				}

				const message =
					error instanceof Error
						? error.message
						: "Unknown status error";

				setState({ kind: "error", message });
			});

		return () => {
			active = false;
		};
	}, []);

	if (state.kind === "loading") {
		return <main>Loading CapstonePM status...</main>;
	}

	if (state.kind === "error") {
		return (
			<main>
				<h1>CapstonePM</h1>
				<p>Unable to load system status.</p>
				<p>{state.message}</p>
			</main>
		);
	}

	return (
		<main>
			<h1>CapstonePM</h1>
			<p>Walking skeleton is running.</p>

			<dl>
				<dt>API</dt>
				<dd>{state.status.api}</dd>

				<dt>Database</dt>
				<dd>{state.status.database}</dd>
			</dl>
		</main>
	);
}