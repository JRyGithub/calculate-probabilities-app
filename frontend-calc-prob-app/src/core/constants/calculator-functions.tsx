export const ProbabilityCalcFunctions = {
  intersection: 'intersection',
  union: 'union',
} as const;

export type ProbabilityCalcFunctions = keyof typeof ProbabilityCalcFunctions;
