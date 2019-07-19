SELECT SLSCL.CLIENTREF, MIN(CL.DEFINITION_), MIN(SA.X), MIN(SA.Y) FROM LG_814_SLSCLREL AS SLSCL
                                INNER JOIN LG_SLSMAN AS SLS ON SLS.LOGICALREF = SLSCL.SALESMANREF 
                                INNER JOIN LG_814_CLCARD AS CL ON CL.LOGICALREF = SLSCL.CLIENTREF 
                                INNER JOIN LG_814_SHIPINFO AS SHP ON SHP.CLIENTREF = CL.LOGICALREF
                                INNER JOIN LG_XT001_814 AS SA ON SA.PARLOGREF = SHP.LOGICALREF
                             WHERE SLSCL.VISITDAY = 1
                                AND SLSCL.VISITPERIOD = 7
                               AND SLS.ACTIVE = 0
                                AND SLS.FIRMNR = 814
                                AND ISNULL(SA.X, '') != ''
                                AND ISNULL(SA.Y, '') != ''
                             GROUP BY SLSCL.CLIENTREF

----------------------------------------------------------------------------------------------------------------

SELECT SLSCL.CLIENTREF, MIN(CL.DEFINITION_), MIN(SA.X), MIN(SA.Y) FROM LG_814_SLSCLREL AS SLSCL
                                INNER JOIN LG_SLSMAN AS SLS ON SLS.LOGICALREF = SLSCL.SALESMANREF 
                                INNER JOIN LG_814_CLCARD AS CL ON CL.LOGICALREF = SLSCL.CLIENTREF 
                                INNER JOIN LG_814_SHIPINFO AS SHP ON SHP.CLIENTREF = CL.LOGICALREF
                                INNER JOIN LG_XT001_814 AS SA ON SA.PARLOGREF = SHP.LOGICALREF
                             WHERE SLS.ACTIVE = 0
                                AND SLS.FIRMNR = 814
                                AND ISNULL(SA.X, '') != ''
                                AND ISNULL(SA.Y, '') != ''
                             GROUP BY SLSCL.CLIENTREF
