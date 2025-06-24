import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import MachineList from '../MachineList';
import * as api from '../api';

jest.mock('../api');

const mockedMachines = [
  {
    id: 1,
    name: 'Machine A',
    serialNumber: '1234',
    type: 'Cutter',
    isActive: true,
  },
  {
    id: 2,
    name: 'Machine B',
    serialNumber: '5678',
    type: 'Assembler',
    isActive: false,
  },
];

describe('MachineList Component', () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('should render the machine list', async () => {
    jest.spyOn(api, 'fetchMachines').mockResolvedValueOnce(mockedMachines);

    render(<MachineList />);

    await waitFor(() => {
      expect(screen.getByText('Machine A')).toBeInTheDocument();
      expect(screen.getByText('Machine B')).toBeInTheDocument();
    });
  });

  it('should filter machines by status', async () => {
    jest.spyOn(api, 'fetchMachines').mockResolvedValueOnce(mockedMachines);

    render(<MachineList />);

    await waitFor(() => {
      expect(screen.getByText('Machine A')).toBeInTheDocument();
    });

    fireEvent.change(screen.getByTestId('status-select'), {
      target: { value: 'active' },
    });

    await waitFor(() => {
      expect(screen.getByText('Machine A')).toBeInTheDocument();
      expect(screen.queryByText('Machine B')).not.toBeInTheDocument();
    });

    fireEvent.change(screen.getByTestId('status-select'), {
      target: { value: 'inactive' },
    });

    await waitFor(() => {
      expect(screen.queryByText('Machine A')).not.toBeInTheDocument();
      expect(screen.getByText('Machine B')).toBeInTheDocument();
    });
  });

  it('should show error toast when API fails', async () => {
    (api.fetchMachines as jest.Mock).mockRejectedValueOnce(new Error('API failed'));

    render(<MachineList />);

    await waitFor(() => {
      expect(screen.getByText(/error while trying to reload the machines/i)).toBeInTheDocument();
    });
  });
});
