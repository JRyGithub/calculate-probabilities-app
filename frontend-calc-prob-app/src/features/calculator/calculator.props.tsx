import { ProbabilityCalcFunctions } from '@/core/constants/calculator-functions';

export const CalculatorFormSubmissionTypes: Array<{
  label: string;
  value: ProbabilityCalcFunctions;
}> = [
  { label: '(P(A)P(B))', value: ProbabilityCalcFunctions.intersection },
  { label: '(P(A) + P(B) - P(A)P(B))', value: ProbabilityCalcFunctions.union },
];
