import type { ProbabilityCalcFunctions } from '../constants/calculator-functions';

export type CalculationResponse = {
  calculation: ProbabilityCalcFunctions;
  errors: string[];
  result: number;
};
