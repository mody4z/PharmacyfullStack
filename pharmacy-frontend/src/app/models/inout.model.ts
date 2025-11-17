export interface InOut {
  id?: number;
  medicineId: number;
  medicineName?: string;
  employeeId: number;
  employeeName?: string;
  clientId?: number;
  clientName?: string;
  transactionType: string;
  quantity: number;
  transactionDate: string;
  notes?: string;
}

export interface CreateInOutDto {
  medicineId: number;
  employeeId: number;
  clientId?: number;
  transactionType: string;
  quantity: number;
  notes?: string;
}
