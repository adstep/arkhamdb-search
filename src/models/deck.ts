export interface Deck {
    id: number;
    name: string;
    likes: number;
    favorites: number;
    comments: number;
    version: string;
    published: Date;
}

export function loadDecks(investigatorId: string): Promise<Deck[]> {
    return fetch(`${window.location.href}/${investigatorId}.json`)
        .then(
            (response) => {
                return response.json();
            }
        )
        .then(
            (decks: Deck[]) => {
                return decks.map(d => ({
                    ...d,
                    name: d.name.replaceAll('&quote;', '"'),
                    published: new Date(d.published)
                }));
            }
        )
        .catch(
            (error) => {
                console.warn(`No decks found for investigator: ${investigatorId}.`);
                return [];
            }
        );
}