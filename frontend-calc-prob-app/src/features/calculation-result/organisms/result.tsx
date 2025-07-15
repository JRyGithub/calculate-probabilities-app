type ResultProps = {
  isLoading: boolean;
  result: number;
};

const Result = ({ isLoading, result }: ResultProps) => {
  return (
    <div className='mt-10'>
      {isLoading && (
        <div className='flex flex-col items-center space-y-4'>
          <span className='brutalist-text animate-pulse'>Calculating...</span>
        </div>
      )}
      {result != null && !isLoading && (
        <div className='text-center'>
          <div className='brutalist-text text-blue-300 text-8xl lg:text-9xl xl:text-[12rem]'>
            {result.toFixed(2)}
          </div>

          <div className='brutalist-text text-lg text-blue-300 md:text-2xl lg:text-3xl xl:text-4xl mt-4'>
            Probability Result
          </div>
        </div>
      )}
    </div>
  );
};

export default Result;
