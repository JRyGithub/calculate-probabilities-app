import { Toaster } from 'sonner';
import './App.css';
import Layout from './infrastructure/layout';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

const App = () => {
  const queryClient = new QueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <Layout />
      <Toaster
        position='top-center'
        richColors
        expand={false}
        duration={4000}
        theme='light'
      />
    </QueryClientProvider>
  );
};

export default App;
