<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:dc="SFA/ILR/2017-18" 
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" exclude-result-prefixes="dc">
<xsl:output method="xml" encoding="UTF-8" indent="yes" />
  <xsl:strip-space elements="*"/>
  <xsl:template match="/dc:Message">
<Message xmlns="SFA/ILR/2017-18">
<Header >
    <CollectionDetails>
       <Collection><xsl:value-of select="dc:Header/dc:CollectionDetails/dc:Collection"/> </Collection>
       <Year><xsl:value-of select="dc:Header/dc:CollectionDetails/dc:Year"/> </Year>
      <FilePreparationDate>
        <xsl:value-of select="dc:Header/dc:CollectionDetails/dc:FilePreparationDate"/>
      </FilePreparationDate>
    </CollectionDetails>
    <Source>
			<ProtectiveMarking><xsl:value-of select="dc:Header/dc:Source/dc:ProtectiveMarking"/></ProtectiveMarking>
			<UKPRN><xsl:value-of select="dc:Header/dc:Source/dc:UKPRN"/></UKPRN>
			<SoftwareSupplier><xsl:value-of select="dc:Header/dc:Source/dc:SoftwareSupplier"/></SoftwareSupplier>
			<SoftwarePackage><xsl:value-of select="dc:Header/dc:Source/dc:SoftwarePackage"/></SoftwarePackage>
			<Release><xsl:value-of select="dc:Header/dc:Source/dc:Release"/></Release>
			<SerialNo><xsl:value-of select="dc:Header/dc:Source/dc:SerialNo"/></SerialNo>
			<DateTime><xsl:value-of select="dc:Header/dc:Source/dc:DateTime"/></DateTime>
	</Source>
	</Header>
	<LearningProvider>
		<UKPRN><xsl:value-of select="dc:LearningProvider/dc:UKPRN"/></UKPRN>
	</LearningProvider>
  <xsl:for-each select="dc:Learner">
    <Learner>
      <LearnRefNumber>
        <xsl:value-of select="dc:LearnRefNumber"/>
      </LearnRefNumber>
      <xsl:if test="PrevLearnRefNumber != ''">
        <PrevLearnRefNumber>
          <xsl:value-of select="dc:PrevLearnRefNumber"/>
        </PrevLearnRefNumber>
      </xsl:if>
      <xsl:if test="PrevUKPRN != ''">
      <PrevUKPRN>
        <xsl:value-of select="dc:PrevUKPRN"/>
      </PrevUKPRN>
      </xsl:if>
      <xsl:if test="PMUKPRN != ''">
      <PMUKPRN>
        <xsl:value-of select="dc:PMUKPRN"/>
      </PMUKPRN>
      </xsl:if>
      <ULN>
        <xsl:value-of select="dc:ULN"/>
      </ULN>
      <xsl:if test="FamilyName != ''">
      <FamilyName><xsl:value-of select="dc:FamilyName"/></FamilyName>
      </xsl:if>
      <xsl:if test="GivenNames != ''">
      <GivenNames>
        <xsl:value-of select="dc:GivenNames"/>
      </GivenNames>
      </xsl:if>
      <xsl:if test="DateOfBirth != ''">
      <DateOfBirth>
        <xsl:value-of select="dc:DateOfBirth"/>
      </DateOfBirth>
      </xsl:if>
      <Ethnicity>
        <xsl:value-of select="dc:Ethnicity"/>
      </Ethnicity>
      <Sex>
        <xsl:value-of select="dc:Sex"/>
      </Sex>
      <LLDDHealthProb>
        <xsl:value-of select="dc:LLDDHealthProb"/>
      </LLDDHealthProb>
      <xsl:if test="NINumber != ''">
        <NINumber>
          <xsl:value-of select="dc:NINumber"/>
        </NINumber>
      </xsl:if>
      <xsl:if test="PriorAttain != ''">
        <PriorAttain>
          <xsl:value-of select="dc:PriorAttain"/>
        </PriorAttain>
      </xsl:if>
      <xsl:if test="Accom != ''">
        <Accom>
          <xsl:value-of select="dc:Accom"/>
        </Accom>
      </xsl:if>
      <xsl:if test="ALSCost != ''">
        <ALSCost>
          <xsl:value-of select="dc:ALSCost"/>
        </ALSCost>
      </xsl:if>
      <xsl:if test="PlanLearnHours != ''">
        <PlanLearnHours>
          <xsl:value-of select="dc:PlanLearnHours"/>
        </PlanLearnHours>
      </xsl:if>
      <xsl:if test="PlanEEPHours != ''">
        <PlanEEPHours>
          <xsl:value-of select="dc:PlanEEPHours"/>
        </PlanEEPHours>
      </xsl:if>
      <xsl:if test="MathGrade != ''">
        <MathGrade>
          <xsl:value-of select="dc:MathGrade"/>
        </MathGrade>
      </xsl:if>
      <xsl:if test="EngGrade != ''">
        <EngGrade>
          <xsl:value-of select="dc:EngGrade"/>
        </EngGrade>
      </xsl:if>
      <PostcodePrior>
        <xsl:value-of select="dc:PostcodePrior"/>
      </PostcodePrior>
      <Postcode>
        <xsl:value-of select="dc:Postcode"/>
      </Postcode>
      <xsl:if test="AddLine1 != ''">
        <AddLine1>
          <xsl:value-of select="dc:AddLine1"/>
        </AddLine1>
      </xsl:if>
      <xsl:if test="AddLine2 != ''">
        <AddLine2>
          <xsl:value-of select="dc:AddLine2"/>
        </AddLine2>
      </xsl:if>
      <xsl:if test="AddLine3 != ''">
        <AddLine3>
          <xsl:value-of select="dc:AddLine3"/>
        </AddLine3>
      </xsl:if>
      <xsl:if test="AddLine4 != ''">
        <AddLine4>
          <xsl:value-of select="dc:AddLine4"/>
        </AddLine4>
      </xsl:if>
      <xsl:if test="TelNo != ''">
        <TelNo>
          <xsl:value-of select="dc:TelNo"/>
        </TelNo>
      </xsl:if>
      <xsl:if test="Email != ''">
        <Email>
          <xsl:value-of select="dc:Email"/>
        </Email>
      </xsl:if>
      <xsl:for-each select="dc:ContactPreference">
        <ContactPreference>
          <ContPrefType>
            <xsl:value-of select="dc:ContPrefType"/>
          </ContPrefType>
          <ContPrefCode>
            <xsl:value-of select="dc:ContPrefCode"/>
          </ContPrefCode>
        </ContactPreference>
      </xsl:for-each>
      <xsl:for-each select="dc:LLDDandHealthProblem">
      <LLDDandHealthProblem>
        <LLDDCat>
          <xsl:value-of select="dc:LLDDCat"/>
        </LLDDCat>
        <xsl:if test="PrimaryLLDD != ''">
        <PrimaryLLDD>
          <xsl:value-of select="dc:PrimaryLLDD"/>
        </PrimaryLLDD>
        </xsl:if>
      </LLDDandHealthProblem>
      </xsl:for-each>
      <xsl:for-each select="dc:LearnerFAM">
      <LearnerFAM>
        <LearnFAMType>
          <xsl:value-of select="dc:LearnFAMType"/>
        </LearnFAMType>
        <LearnFAMCode>
          <xsl:value-of select="dc:LearnFAMCode"/> 
        </LearnFAMCode>
      </LearnerFAM>
      </xsl:for-each>
      <xsl:for-each select="dc:ProviderSpecLearnerMonitoring">
        <ProviderSpecLearnerMonitoring>
          <ProvSpecLearnMonOccur>
            <xsl:value-of select="dc:ProvSpecLearnMonOccur"/>
          </ProvSpecLearnMonOccur>
          <ProvSpecLearnMon>
            <xsl:value-of select="dc:ProvSpecLearnMon"/>
          </ProvSpecLearnMon>
        </ProviderSpecLearnerMonitoring>
      </xsl:for-each>
      <xsl:for-each select="dc:LearnerEmploymentStatus">
        <LearnerEmploymentStatus>
          <EmpStat>
            <xsl:value-of select="dc:EmpStat"/>
          </EmpStat>
          <DateEmpStatApp>
            <xsl:value-of select="dc:DateEmpStatApp"/>
          </DateEmpStatApp>
          <xsl:if test="EmpId != ''">
            <EmpId>
              <xsl:value-of select="dc:EmpId"/>
            </EmpId>
          </xsl:if>
          <xsl:for-each select="dc:EmploymentStatusMonitoring">
          <EmploymentStatusMonitoring>
            <ESMType>
              <xsl:value-of select="dc:ESMType"/>
            </ESMType>
            <ESMCode>
              <xsl:value-of select="dc:ESMCode"/>
            </ESMCode>
          </EmploymentStatusMonitoring>
          </xsl:for-each>
        </LearnerEmploymentStatus>
      </xsl:for-each>
      <xsl:for-each select="dc:LearnerHE">
        <LearnerHE>
          <xsl:if test="UCASPERID != ''">
            <UCASPERID>
              <xsl:value-of select="dc:UCASPERID"/>
            </UCASPERID>
          </xsl:if>
          <xsl:if test="TTACCOM != ''">
            <TTACCOM>
              <xsl:value-of select="dc:TTACCOM"/>
            </TTACCOM>
          </xsl:if>
          <xsl:for-each select="dc:LearnerHEFinancialSupport">
            <LearnerHEFinancialSupport>
              <FINTYPE>
                <xsl:value-of select="dc:FINTYPE"/>
              </FINTYPE>
              <FINAMOUNT>
                <xsl:value-of select="dc:FINAMOUNT"/>
              </FINAMOUNT>
            </LearnerHEFinancialSupport>
          </xsl:for-each>
        </LearnerHE>
      </xsl:for-each>
      <xsl:for-each select="dc:LearningDelivery">
        <LearningDelivery>
          <LearnAimRef>
            <xsl:value-of select="dc:LearnAimRef"/>
          </LearnAimRef>
          <AimType>
            <xsl:value-of select="dc:AimType"/>
          </AimType>
          <AimSeqNumber>
            <xsl:value-of select="dc:AimSeqNumber"/>
          </AimSeqNumber>
          <LearnStartDate>
            <xsl:value-of select="dc:LearnStartDate"/>
          </LearnStartDate>
          <xsl:if test="OrigLearnStartDate != ''">
          <OrigLearnStartDate>
            <xsl:value-of select="dc:OrigLearnStartDate"/>
          </OrigLearnStartDate>
          </xsl:if>
          <LearnPlanEndDate>
            <xsl:value-of select="dc:LearnPlanEndDate"/>
          </LearnPlanEndDate>
          <FundModel><xsl:value-of select="dc:FundModel"/></FundModel>
          <xsl:if test="ProgType != ''">
            <ProgType>
              <xsl:value-of select="dc:ProgType"/>
            </ProgType>
          </xsl:if>
          <xsl:if test="FworkCode != ''">
            <FworkCode>
              <xsl:value-of select="dc:FworkCode"/>
            </FworkCode>
          </xsl:if>
          <xsl:if test="PwayCode != ''">
            <PwayCode>
              <xsl:value-of select="dc:PwayCode"/>
            </PwayCode>
          </xsl:if>
          <xsl:if test="StdCode != ''">
            <StdCode>
              <xsl:value-of select="dc:StdCode"/>
            </StdCode>
          </xsl:if>
          <xsl:if test="PartnerUKPRN != ''">
            <PartnerUKPRN>
              <xsl:value-of select="dc:PartnerUKPRN"/>
            </PartnerUKPRN>
          </xsl:if>
          <DelLocPostCode>
            <xsl:value-of select="dc:DelLocPostCode"/>
          </DelLocPostCode>
          <xsl:if test="AddHours != ''">
            <AddHours>
              <xsl:value-of select="dc:AddHours"/>
            </AddHours>
          </xsl:if>
          <xsl:if test="PriorLearnFundAdj != ''">
            <PriorLearnFundAdj>
              <xsl:value-of select="dc:PriorLearnFundAdj"/>
            </PriorLearnFundAdj>
          </xsl:if>
          <xsl:if test="OtherFundAdj != ''">
          <OtherFundAdj>
            <xsl:value-of select="dc:OtherFundAdj"/>
          </OtherFundAdj>
          </xsl:if>
          <xsl:if test="ConRefNumber != ''">
          <ConRefNumber>
            <xsl:value-of select="dc:ConRefNumber"/>
          </ConRefNumber>
          </xsl:if>
          <xsl:if test="EPAOrgID != ''">
          <EPAOrgID>
            <xsl:value-of select="dc:EPAOrgID"/>
          </EPAOrgID>
          </xsl:if>
          <xsl:if test="EmpOutcome != ''">
          <EmpOutcome>
            <xsl:value-of select="dc:EmpOutcome"/>
          </EmpOutcome>
          </xsl:if>
          <CompStatus>
            <xsl:value-of select="dc:CompStatus"/>
          </CompStatus>
          <xsl:if test="LearnActEndDate != ''">
            <LearnActEndDate>
              <xsl:value-of select="dc:LearnActEndDate"/>
            </LearnActEndDate>
          </xsl:if>
          <xsl:if test="WithdrawReason != ''">
          <WithdrawReason>
            <xsl:value-of select="dc:WithdrawReason"/>
          </WithdrawReason>
          </xsl:if>
          <xsl:if test="Outcome != ''">
          <Outcome>
            <xsl:value-of select="dc:Outcome"/>
          </Outcome>
          </xsl:if>
          <xsl:if test="AchDate != ''">
          <AchDate>
            <xsl:value-of select="dc:AchDate"/>
          </AchDate>
          </xsl:if>
          <xsl:if test="OutGrade != ''">
          <OutGrade>
            <xsl:value-of select="dc:OutGrade"/>
          </OutGrade>
          </xsl:if>
          <xsl:if test="SWSupAimId != ''">
          <SWSupAimId>
            <xsl:value-of select="dc:SWSupAimId"/>
          </SWSupAimId>
          </xsl:if>
            <xsl:for-each select="dc:LearningDeliveryFAM">
            <LearningDeliveryFAM>
            <LearnDelFAMType>
              <xsl:value-of select="dc:LearnDelFAMType"/>
            </LearnDelFAMType>
            <LearnDelFAMCode>
              <xsl:value-of select="dc:LearnDelFAMCode"/>
            </LearnDelFAMCode>
              <xsl:if test="LearnDelFAMDateFrom != ''">
                <LearnDelFAMDateFrom>
                  <xsl:value-of select="dc:LearnDelFAMDateFrom"/>
                </LearnDelFAMDateFrom>
              </xsl:if>
              <xsl:if test="LearnDelFAMDateTo != ''">
                <LearnDelFAMDateTo>
                  <xsl:value-of select="dc:LearnDelFAMDateTo"/>
                </LearnDelFAMDateTo>
              </xsl:if>
          </LearningDeliveryFAM>
            </xsl:for-each>
            <xsl:for-each select="LearningDeliveryWorkPlacement">
            <LearningDeliveryWorkPlacement>
              <WorkPlaceStartDate>
                <xsl:value-of select="dc:WorkPlaceStartDate"/>
              </WorkPlaceStartDate>
              <xsl:if test="WorkPlaceEndDate != ''">
                <WorkPlaceEndDate>
                  <xsl:value-of select="dc:WorkPlaceEndDate"/>
                </WorkPlaceEndDate>
              </xsl:if>
              <WorkPlaceHours>
                <xsl:value-of select="dc:WorkPlaceHours"/>
              </WorkPlaceHours>
              <WorkPlaceMode>
                <xsl:value-of select="dc:WorkPlaceMode"/>
              </WorkPlaceMode>
              <xsl:if test="WorkPlaceEmpId != ''">
                <WorkPlaceEmpId>
                  <xsl:value-of select="dc:WorkPlaceEmpId"/>
                </WorkPlaceEmpId>
              </xsl:if>
            </LearningDeliveryWorkPlacement>
          </xsl:for-each>
            <xsl:for-each select="AppFinRecord">
            <AppFinRecord>
              <AFinType>
                <xsl:value-of select="dc:AFinType"/>
              </AFinType>
              <AFinCode>
                <xsl:value-of select="dc:AFinCode"/>
              </AFinCode>
              <AFinDate>
                <xsl:value-of select="dc:AFinDate"/>
              </AFinDate>
              <AFinAmount>
                <xsl:value-of select="dc:AFinAmount"/>
              </AFinAmount>
            </AppFinRecord>
            </xsl:for-each>
            <xsl:for-each select="dc:ProviderSpecDeliveryMonitoring">
            <ProviderSpecDeliveryMonitoring>
              <ProvSpecDelMonOccur>
                <xsl:value-of select="dc:ProvSpecDelMonOccur"/>
              </ProvSpecDelMonOccur>
              <ProvSpecDelMon>
                <xsl:value-of select="dc:ProvSpecDelMon"/>
              </ProvSpecDelMon>
            </ProviderSpecDeliveryMonitoring>
          </xsl:for-each>
          <xsl:for-each select="dc:LearningDeliveryHE">
            <LearningDeliveryHE>
              <xsl:if test="NUMHUS != ''">
                <NUMHUS>
                  <xsl:value-of select="dc:NUMHUS"/>
                </NUMHUS>
              </xsl:if>
              <xsl:if test="SSN != ''">
                <SSN>
                  <xsl:value-of select="dc:SSN"/>
                </SSN>
              </xsl:if>
              <xsl:if test="QUALENT3 != ''">
                <QUALENT3>
                  <xsl:value-of select="dc:QUALENT3"/>
                </QUALENT3>
              </xsl:if>
              <xsl:if test="SOC2000 != ''">
                <SOC2000>
                  <xsl:value-of select="dc:SOC2000"/>
                </SOC2000>
              </xsl:if>
              <xsl:if test="SEC != ''">
                <SEC>
                  <xsl:value-of select="dc:SEC"/>
                </SEC>
              </xsl:if>
              <xsl:if test="UCASAPPID  != ''">
                <UCASAPPID >
                  <xsl:value-of select="dc:UCASAPPID "/>
                </UCASAPPID >
              </xsl:if>
              <TYPEYR>
                <xsl:value-of select="dc:TYPEYR"/>
              </TYPEYR>
              <MODESTUD>
                <xsl:value-of select="dc:MODESTUD"/>
              </MODESTUD>
              <FUNDLEV>
                <xsl:value-of select="dc:FUNDLEV"/>
              </FUNDLEV>
              <FUNDCOMP>
                <xsl:value-of select="dc:FUNDCOMP"/>
              </FUNDCOMP>
              <xsl:if test="STULOAD  != ''">
                <STULOAD >
                  <xsl:value-of select="dc:STULOAD "/>
                </STULOAD >
              </xsl:if>
              <YEARSTU>
                <xsl:value-of select="dc:YEARSTU"/>
              </YEARSTU>
              <MSTUFEE>
                <xsl:value-of select="dc:MSTUFEE"/>
              </MSTUFEE>
              <xsl:if test="PCOLAB != ''">
                <PCOLAB>
                  <xsl:value-of select="dc:PCOLAB"/>
                </PCOLAB>
              </xsl:if>
              <xsl:if test="PCFLDCS != ''">
                <PCFLDCS>
                  <xsl:value-of select="dc:PCFLDCS"/>
                </PCFLDCS>
              </xsl:if>
              <xsl:if test="PCSLDCS != ''">
                <PCSLDCS>
                  <xsl:value-of select="dc:PCSLDCS"/>
                </PCSLDCS>
              </xsl:if>
              <xsl:if test="PCTLDCS  != ''">
                <PCTLDCS >
                  <xsl:value-of select="dc:PCTLDCS "/>
                </PCTLDCS >
              </xsl:if>
              <SPECFEE>
                <xsl:value-of select="dc:SPECFEE"/>
              </SPECFEE>
              <xsl:if test="NETFEE != ''">
                <NETFEE>
                  <xsl:value-of select="dc:NETFEE"/>
                </NETFEE>
              </xsl:if>
              <xsl:if test="GROSSFEE != ''">
                <GROSSFEE>
                  <xsl:value-of select="dc:GROSSFEE"/>
                </GROSSFEE>
              </xsl:if>
              <xsl:if test="DOMICILE != ''">
                <DOMICILE>
                  <xsl:value-of select="dc:DOMICILE"/>
                </DOMICILE>
              </xsl:if>
              <xsl:if test="ELQ != ''">
                <ELQ>
                  <xsl:value-of select="dc:ELQ"/>
                </ELQ>
              </xsl:if>
              <xsl:if test="HEPostCode  != ''">
                <HEPostCode >
                  <xsl:value-of select="dc:HEPostCode "/>
                </HEPostCode >
              </xsl:if>
            </LearningDeliveryHE>
          </xsl:for-each>
      </LearningDelivery>       
      </xsl:for-each>
    </Learner>
  </xsl:for-each>
  <xsl:for-each select="dc:LearnerDestinationandProgression">
    <LearnerDestinationandProgression>
      <LearnRefNumber>
        <xsl:value-of select="dc:LearnRefNumber"/>
      </LearnRefNumber>
      <ULN>
        <xsl:value-of select="dc:ULN"/>
      </ULN>
      <xsl:for-each select="dc:DPOutcome">
        <DPOutcome>
          <OutType>
            <xsl:value-of select="dc:OutType"/>
          </OutType>
          <OutCode>
            <xsl:value-of select="dc:OutCode"/>
          </OutCode>
          <OutStartDate>
            <xsl:value-of select="dc:OutStartDate"/>
          </OutStartDate>
          <xsl:if test="OutEndDate  != ''">
            <OutEndDate >
              <xsl:value-of select="dc:OutEndDate "/>
            </OutEndDate >
          </xsl:if>
          <OutCollDate>
            <xsl:value-of select="dc:OutCollDate"/>
          </OutCollDate>
        </DPOutcome>
      </xsl:for-each>
    </LearnerDestinationandProgression>
  </xsl:for-each>
</Message>
</xsl:template>


</xsl:stylesheet>