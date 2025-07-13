import React from 'react';
import { useFormContext } from 'react-hook-form';
import { FormField, FormLabel, FormMessage } from '../ui/form';
import { Input } from '../ui/input';

type RHFInputProps = {
  name: string;
  label?: string;
  className?: string;
} & React.InputHTMLAttributes<HTMLInputElement>;

const RHFInput = ({ name, type = 'text', label, className }: RHFInputProps) => {
  const { register } = useFormContext();
  return (
    <FormField
      {...register(name)}
      render={({ field }) => {
        return (
          <div className='flex flex-col gap-2 w-40'>
            <FormLabel htmlFor={field.name} className='self-start'>
              {label}
            </FormLabel>
            <Input
              variant='brutalist'
              type={type}
              className={className}
              {...field}
            />
            <div className='h-5'>
              <FormMessage />
            </div>
          </div>
        );
      }}
    />
  );
};

export default RHFInput;
