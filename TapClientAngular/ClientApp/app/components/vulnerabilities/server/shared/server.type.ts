import { SeverityLevel } from '../../shared/severity-level.enum';

export class Server{
    id: string;
    ipAddress: string;
    netBIOSName: string;
    numberOfFindings: number;
    highestSeverity: SeverityLevel;
    oldestFindingDate: string;
    status: string;
    decision: string;
    comments: string;
}