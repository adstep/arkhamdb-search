import investigatorsData from '../data/investigators.json'

export interface Investigator {
    id: string;
    name: string;
    subName: string;
    faction: string;
    set: string;
}

export function loadInvestigatorMap(): Map<string, Investigator[]> {
    const result: Map<string, Investigator[]> = new Map<string, Investigator[]>();

    (investigatorsData as Investigator[]).forEach((investigator) => {
      if (result.has(investigator.name)) {
        result.get(investigator.name)?.push(investigator);
      } else {
        result.set(investigator.name, [investigator]);
      }
    });

    return result;
}


