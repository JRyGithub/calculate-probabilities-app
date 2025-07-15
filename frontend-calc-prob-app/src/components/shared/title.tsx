import { cn } from '@/lib/utils';
import { type PropsWithChildren } from 'react';

type TitleProps = {
  className?: string;
};

const Title = ({ className, children }: PropsWithChildren<TitleProps>) => {
  return (
    <h1
      className={cn(
        'text-4xl md:text-6xl font-extrabold uppercase text-[#FF4911] tracking-wide',
        className
      )}
    >
      {children}
    </h1>
  );
};

export default Title;
