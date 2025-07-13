import { z } from 'zod';
import { ProbabilityCalcFunctions } from '@/core/constants/calculator-functions';

export const CalculatorAction = z.enum(Object.keys(ProbabilityCalcFunctions));

export const ProbabilityCalculatorFormSchema = z.object({
  probabilityA: z
    .string()
    .min(1, 'Probability A is required')
    .refine(
      val => !isNaN(Number(val)) && isFinite(Number(val)),
      'Probability A must be a valid number'
    )
    .refine(
      val => Number(val) >= 0 && Number(val) <= 1,
      'Probability A must be between 0 and 1'
    ),
  probabilityB: z
    .string()
    .min(1, 'Probability B is required')
    .refine(
      val => !isNaN(Number(val)) && isFinite(Number(val)),
      'Probability B must be a valid number'
    )
    .refine(
      val => Number(val) >= 0 && Number(val) <= 1,
      'Probability B must be between 0 and 1'
    ),
  action: CalculatorAction.optional(),
});

export type ProbabilityCalculatorFormInputs = z.infer<
  typeof ProbabilityCalculatorFormSchema
>;
