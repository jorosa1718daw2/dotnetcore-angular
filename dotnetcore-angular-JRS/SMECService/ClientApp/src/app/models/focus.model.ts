import { Analyzer } from './analyzer.model';

export interface Focus {
  focusId: number;
  name: string;
  description: string;
  analyzers: Analyzer[];
}
