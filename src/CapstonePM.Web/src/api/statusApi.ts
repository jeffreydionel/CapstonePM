export type StatusResponse = {
	api: string;
	database: string;
};

export async function getStatus(): Promise<StatusResponse> {
	const response = await fetch("/api/status");

	if (!response.ok) {
		throw new Error(
			`Status request failed with HTTP ${response.status}`,
		);
	}

	return (await response.json()) as StatusResponse;
}